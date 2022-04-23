using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mosaik.idAPI.Services
{
    public class MosaikRepository : IMosaikRepository
    {
        private readonly IDataContext _context;
        public class Account
        {
            public string Email {get; set;}
            public string Username {get; set;}
        }

        public MosaikRepository(IDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MosaikParent>> getAll()
        {
            return await _context.MosaikParents.ToListAsync();
        }

        public async Task InsertAccount(MosaikParent mosaikParent)
        {
            _context.MosaikParents.Add(mosaikParent);
            MosaikUser mosaikUser = new()
            {
                Email = mosaikParent.Email,
                AccountStatus = "Parent"
            };
            _context.MosaikUsers.Add(mosaikUser);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAccount(MosaikChild mosaikChild)
        {
            _context.MosaikChildren.Add(mosaikChild);
            MosaikUser mosaikUser = new()
            {
                Email = mosaikChild.Email,
                AccountStatus = "Child"
            };
            _context.MosaikUsers.Add(mosaikUser);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<int, String, String>> AuthenticateAccount(string Email, string password)
        {
            var list = await _context.MosaikUsers.ToListAsync();
            foreach (var mosaikItem in list)
            {
                if (mosaikItem.Email == Email)
                {
                    if(mosaikItem.AccountStatus == "Child") {
                        var children = await _context.MosaikChildren.ToListAsync();
                        foreach (var mosaikChild in children)
                        {
                            if (mosaikChild.Password == password && mosaikChild.Email == Email) 
                            {
                                return new Tuple<int, string, string> (mosaikChild.MosaikChildID, "child", mosaikChild.Username);
                            }
                        }
                    } else if (mosaikItem.AccountStatus == "Parent") {
                        var parents = await _context.MosaikParents.ToListAsync();
                        foreach (var mosaikParent in parents)
                        {
                            if (mosaikParent.Password == password && mosaikParent.Email == Email) 
                            {
                                return new Tuple<int, string, string> (mosaikParent.MosaikParentID, "supervisor", mosaikParent.Username);
                            }
                        }
                    }
                }
            }
            return new Tuple<int, string, string> (0, "false", "null");
        }

        public async Task<List<Account>> GetSupervisedRequests(int mosaikChildID) 
        {
            var list = await _context.MosaikParentsChildren.ToListAsync();
            List<Account> supervisedRequests = new List<Account>();
            foreach (var mosaikParentChild in list) 
            {
                if (mosaikParentChild.childID == mosaikChildID)
                {
                    
                    MosaikParent mosaikParent = await _context.MosaikParents.FindAsync(mosaikParentChild.parentID);

                    Account account = new()
                    {
                        Email = mosaikParent.Email,
                        Username = mosaikParent.Username
                    };
                    supervisedRequests.Add(account);
                } 
            }
            return supervisedRequests;
        }
        public async Task<List<Account>> GetSupervisorAccounts(int mosaikParentID)
        {
            var list = await _context.MosaikParentsChildren.ToListAsync();
            List<Account> supervisorAccounts = new List<Account>();
            foreach (var mosaikParentChild in list) 
            {
                if (mosaikParentChild.parentID == mosaikParentID && mosaikParentChild.Authorized)
                {
                    
                    MosaikChild mosaikChild = await _context.MosaikChildren.FindAsync(mosaikParentChild.childID);

                    Account account = new()
                    {
                        Email = mosaikChild.Email,
                        Username = mosaikChild.Username
                    };
                    supervisorAccounts.Add(account);
                } 
            }
            return supervisorAccounts;
        }
        public async Task<String> Update(string Email, string Username)
        {
            var list = await _context.MosaikUsers.ToListAsync();
            foreach (var item in list)
            {
                if (item.Email == Email)
                {
                    if (item.AccountStatus == "Child")
                    {
                        var children = await _context.MosaikChildren.ToListAsync();
                        foreach (var mosaikChild in children)
                        {
                            if (mosaikChild.Email == Email) 
                            {
                                mosaikChild.Username = Username;
                                await _context.SaveChangesAsync();
                                return "success";
                            }
                        }
                    } 
                    else if (item.AccountStatus == "Parent")
                    {
                        var parents = await _context.MosaikParents.ToListAsync();
                        foreach (var mosaikParent in parents)
                        {
                            if (mosaikParent.Email == Email) 
                            {
                                mosaikParent.Username = Username;
                                await _context.SaveChangesAsync();
                                return "success";
                            }
                        }
                    }
                }
            }
            return "failed";
        }
        public async Task<String> UpdatePass(string Email, string OldPassword, string NewPassword)
        {
            var list = await _context.MosaikUsers.ToListAsync();
            foreach (var item in list)
            {
                if (item.Email == Email)
                {
                    if (item.AccountStatus == "Child")
                    {
                        var children = await _context.MosaikChildren.ToListAsync();
                        foreach (var mosaikChild in children)
                        {
                            if (mosaikChild.Email == Email && mosaikChild.Password != OldPassword)
                            {
                                return "wrong password";
                            }
                            else if (mosaikChild.Email == Email && mosaikChild.Password == OldPassword) 
                            {
                                mosaikChild.Password = NewPassword;
                                await _context.SaveChangesAsync();
                                return "success";
                            }
                        }
                    } 
                    else if (item.AccountStatus == "Parent")
                    {
                        var parents = await _context.MosaikParents.ToListAsync();
                        foreach (var mosaikParent in parents)
                        {
                            if (mosaikParent.Email == Email && mosaikParent.Password != OldPassword)
                            {
                                return "wrong password";
                            }
                            else if (mosaikParent.Email == Email && mosaikParent.Password == OldPassword) 
                            {
                                mosaikParent.Password = NewPassword;
                                await _context.SaveChangesAsync();
                                return "success";
                            }
                        }
                    }
                }
            }
            return "failed";
        }
    }
}

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
                            if (mosaikChild.Password == password) 
                            {
                                return new Tuple<int, string, string> (mosaikChild.MosaikChildID, "child", mosaikChild.Username);
                            }
                            else
                            {
                                return new Tuple<int, string, string> (0, "false", "null");
                            }
                        }
                    } else if (mosaikItem.AccountStatus == "Parent") {
                        var parents = await _context.MosaikParents.ToListAsync();
                        foreach (var mosaikParent in parents)
                        {
                            if (mosaikParent.Password == password) 
                            {
                                return new Tuple<int, string, string> (mosaikParent.MosaikParentID, "supervisor", mosaikParent.Username);
                            }
                            else 
                            {
                                return new Tuple<int, string, string> (0, "false", "null");
                            }
                        }
                    }
                }
            }
            return new Tuple<int, string, string> (0, "false", "null");
        }

        public async Task<Tuple<String, String>[]> GetSupervisedRequests(int mosaikChildID) 
        {
            var list = await _context.MosaikParentsChildren.ToListAsync();
            Tuple<String, String>[] supervisedRequests = {};
            foreach (var mosaikParentChild in list) 
            {
                if (mosaikParentChild.childID == mosaikChildID)
                {
                    supervisedRequests.Append<Tuple<String, String>>
                    (
                        new Tuple<String, String> (_context.MosaikChildren.Find(mosaikChildID).Email, _context.MosaikChildren.Find(mosaikChildID).Username)
                    );
                } 
            }
            return supervisedRequests;
        }
        public async Task<Tuple<String, String>[]> GetSupervisorAccounts(int mosaikParentID)
        {
            var list = await _context.MosaikParentsChildren.ToListAsync();
            Tuple<String, String>[] supervisorAccounts = {};
            foreach (var mosaikParentChild in list) 
            {
                if (mosaikParentChild.parentID == mosaikParentID)
                {
                    supervisorAccounts.Append<Tuple<String, String>>
                    (
                        new Tuple<String, String> (_context.MosaikParents.Find(mosaikParentID).Email, _context.MosaikParents.Find(mosaikParentID).Username)
                    );
                } 
            }
            return supervisorAccounts;
        }
    }
}

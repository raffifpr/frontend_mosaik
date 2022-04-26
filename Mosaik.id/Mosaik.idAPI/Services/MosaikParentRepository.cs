using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mosaik.idAPI.Services
{
    public class MosaikParentRepository : IMosaikParentRepository
    {
        private readonly IDataContext _context;

        public MosaikParentRepository(IDataContext context) 
        {
            _context = context;
        }
        
        public async Task <MosaikParent> Get (string Email)
        {
            var list = await _context.MosaikParents.ToListAsync();
            foreach (var item in list)
            {
                if (item.Email == Email)
                {
                    return item;
                }
            }
            return null;
            // return await _context.MosaikParents.FindAsync(Email);
        }

        public async Task <MosaikParentChild> GetParentChild (int childID, int parentID)
        {
            var list = await _context.MosaikParentsChildren.ToListAsync();
            foreach (var item in list)
            {
                if (item.childID == childID && item.parentID == parentID)
                {
                    return item;
                }
            }
            return null;
            // return await _context.MosaikParents.FindAsync(Email);
        }
        public async Task InsertChildAccount(string Email, MosaikParentChild mosaikParentChild) 
        {
            var list = await _context.MosaikChildren.ToListAsync();
            MosaikChild mosaikChild = null;
            foreach (var item in list)
            {
                if (item.Email == Email)
                {
                    mosaikChild = item;
                }
            }
            if (mosaikChild == null)
                throw new NullReferenceException();
            
            _context.MosaikParentsChildren.Add(mosaikParentChild);
            await _context.SaveChangesAsync();
        }
        public async Task<String> InsertChildAccount(string Email, string ChildEmail) 
        {
            var list = await _context.MosaikParents.ToListAsync();
            foreach (var item in list)
            {
                if (item.Email == Email)
                {
                    var mosaikChildren = await _context.MosaikChildren.ToListAsync();
                    foreach (var mosaikChild in mosaikChildren)
                    {
                        if (mosaikChild.Email == ChildEmail)
                        {
                            if (GetParentChild(mosaikChild.MosaikChildID, item.MosaikParentID) == null)
                            {
                                MosaikParentChild mosaikParentChild = new()
                                {
                                    parentID = item.MosaikParentID,
                                    childID = mosaikChild.MosaikChildID,
                                    Authorized = false,
                                };
                                _context.MosaikParentsChildren.Add(mosaikParentChild);
                                await _context.SaveChangesAsync();
                                return "success";
                            }
                        }
                    }
                    return "don't exist";
                }
            }
            return "failed";
        }
        public async Task<IEnumerable<MosaikParentChild>> GetMosaikChildrenParent()
        {
            return await _context.MosaikParentsChildren.ToListAsync();
        }
        public async Task<String> DeleteChildAccount(string Email, string ChildEmail)
        {
            var list = await _context.MosaikParents.ToListAsync();
            foreach (var item in list)
            {
                if (item.Email == Email)
                {
                    var mosaikChildren = await _context.MosaikChildren.ToListAsync();
                    foreach (var mosaikChild in mosaikChildren)
                    {
                        if (mosaikChild.Email == ChildEmail)
                        {
                            var mosaikParentsChildren = await _context.MosaikParentsChildren.ToListAsync();
                            foreach (var mosaikParentChild in mosaikParentsChildren)
                            {
                                if (mosaikParentChild.parentID == item.MosaikParentID && mosaikParentChild.childID == mosaikChild.MosaikChildID)
                                {    
                                    _context.MosaikParentsChildren.Remove(mosaikParentChild);
                                    await _context.SaveChangesAsync();
                                    return "success";
                                }
                            }
                            return "don't exist";
                        }
                    }
                    return "don't exist";
                }
            }
            return "failed";
        }
    }
}
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
        public async Task<IEnumerable<MosaikParentChild>> GetMosaikChildrenParent()
        {
            return await _context.MosaikParentsChildren.ToListAsync();
        }
        public async Task DeleteChildAccount(string Email)
        {
            var itemToRemove = await _context.MosaikParentsChildren.FindAsync(Email);
            if (itemToRemove == null)
                throw new NullReferenceException();
            
            _context.MosaikParentsChildren.Remove(itemToRemove);
            await _context.SaveChangesAsync();
        }
    }
}
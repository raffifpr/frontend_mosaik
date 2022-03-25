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
            await _context.SaveChangesAsync();
        }

        public async Task InsertAccount(MosaikChild mosaikChild)
        {
            _context.MosaikChildren.Add(mosaikChild);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AuthenticateAccount(string Email, string password)
        {
            var list = await _context.MosaikParents.ToListAsync();
            foreach (var mosaikItem in list)
            {
                if (mosaikItem.Email == Email)
                {
                    if (mosaikItem.Password == password)
                    {
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}

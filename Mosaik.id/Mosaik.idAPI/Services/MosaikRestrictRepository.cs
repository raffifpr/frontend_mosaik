using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mosaik.idAPI.Services
{
    public class MosaikRestrictRepository : IMosaikRestrictRepository
    {
        private readonly IDataContext _context;

        public MosaikRestrictRepository(IDataContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<MosaikChildRestrict>> GetMosaikChildRestricts()
        {
            return await _context.MosaikChildRestricts.ToListAsync();
        }

        public async Task InsertRestrictedLink(MosaikChildRestrict mosaikChildRestrict) 
        {
            _context.MosaikChildRestricts.Add(mosaikChildRestrict);
            await _context.SaveChangesAsync();
        }

        public async Task DisableNotif(MosaikChildRestrict mosaikChildRestrict) 
        {
            var itemToUpdate = await _context.MosaikChildRestricts.FindAsync(mosaikChildRestrict.MosaikChildRestrictID);
            if (itemToUpdate == null)
                throw new NullReferenceException();
            itemToUpdate.Notif = mosaikChildRestrict.Notif;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRestrictedLink(int id)
        {
            var itemToRemove = await _context.MosaikChildRestricts.FindAsync(id);
            if (itemToRemove == null)
                throw new NullReferenceException();
            
            _context.MosaikChildRestricts.Remove(itemToRemove);
            await _context.SaveChangesAsync();
        }

    }
}
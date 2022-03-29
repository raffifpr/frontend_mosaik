using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mosaik.idAPI.Services
{
    public class MosaikHistoryRepository : IMosaikHistoryRepository
    {
        private readonly IDataContext _context;
        public MosaikHistoryRepository(IDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MosaikHistory>> getAll()
        {
            return await _context.MosaikHistories.ToListAsync();
        }

        public async Task InsertHistory(MosaikHistory mosaikHistory)
        {
            _context.MosaikHistories.Add(mosaikHistory);
            await _context.SaveChangesAsync();
        }
        public async Task InsertDateHistory(MosaikDateHistory mosaikDateHistory)
        {
            _context.MosaikDateHistories.Add(mosaikDateHistory);
            await _context.SaveChangesAsync();
        }
    }
}

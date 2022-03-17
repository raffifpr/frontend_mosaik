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

        // private void InitializeData()
        // {
        //     _historyItems = new List<MosaikHistory>();

        //     var item1 = new MosaikHistory
        //     {
        //         ID = 500,
        //         Link = "www.google.com",
        //         AccessedTime = "2000-15-01-15:30:45",
        //     };

        //     _historyItems.Add(item1);
        // }

        public async Task<IEnumerable<MosaikHistory>> getAll()
        {
            return await _context.MosaikHistories.ToListAsync();
        }

        public async Task InsertHistory(MosaikHistory mosaikHistory)
        {
            _context.MosaikHistories.Add(mosaikHistory);
            await _context.SaveChangesAsync();
        }
    }
}

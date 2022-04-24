using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Mosaik.idAPI.Services
{
    public class MosaikChildRepository : IMosaikChildRepository
    {
        private readonly IDataContext _context;

        public MosaikChildRepository(IDataContext context) 
        {
            _context = context;
        }
        public async Task<MosaikChild> GetChildAccount(string Email) 
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
            return mosaikChild;
        }
    }
}
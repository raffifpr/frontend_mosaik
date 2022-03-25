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
            return await _context.MosaikChildren.FindAsync(Email);
        }
    }
}
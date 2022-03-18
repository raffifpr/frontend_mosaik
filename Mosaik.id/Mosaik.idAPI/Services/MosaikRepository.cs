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

        public async Task<IEnumerable<MosaikItem>> getAll()
        {
            return await _context.MosaikItems.ToListAsync();
        }

        // public bool IsAccountValid(MosaikItem mosaikItem)
        // {
        //     return _items.Any(item => item.Email == mosaikItem.Email
        //     && item.Password == mosaikItem.Password);
        // }

        public async Task InsertAccount(MosaikItem mosaikItem)
        {
            _context.MosaikItems.Add(mosaikItem);
            await _context.SaveChangesAsync();
            // if (!CheckUsernameUsed(mosaikItem.Email))
            // {
            //     _items.Add(mosaikItem);
            // }
        }

        public async Task<bool> AuthenticateAccount(string Email, string password)
        {
            var list = await _context.MosaikItems.ToListAsync();
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

        // public bool CheckUsernameUsed(string Email)
        // {
        //     return _items.Any(item => item.Email == Email);
        // }
        // public bool DoesItemExist(int id)
        // {
        //     return _items.Any(item => item.ID == id);
        // }
        // private void InitializeData()
        // {
        //     _items = new List<MosaikItem>();

        //     var item1 = new MosaikItem
        //     {
        //         FullName = "Learn app development",
        //         Email = "Take Microsoft Learn Courses",
        //         Password = "password",
        //         ID = 500,
        //     };

        //     _items.Add(item1);
        // }
    }
}

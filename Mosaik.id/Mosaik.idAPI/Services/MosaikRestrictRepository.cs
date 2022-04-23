using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Data;
using Mosaik.idAPI.Dtos;
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

        public async Task<String> InsertRestrictedLink(string Email, string Link) 
        {
            var children = await _context.MosaikChildren.ToListAsync();
            foreach (var item in children)
            {
                if (item.Email == Email)
                {
                    MosaikChildRestrict mosaikChildRestrict = new()
                    {
                        ChildID = item.MosaikChildID,
                        Link = Link,
                        Notif = true
                    };
                    _context.MosaikChildRestricts.Add(mosaikChildRestrict);
                    await _context.SaveChangesAsync();
                    return "success";
                }
            }
            return "failed";
        }

        public async Task DisableNotif(MosaikChildRestrict mosaikChildRestrict) 
        {
            var itemToUpdate = await _context.MosaikChildRestricts.FindAsync(mosaikChildRestrict.MosaikChildRestrictID);
            if (itemToUpdate == null)
                throw new NullReferenceException();
            itemToUpdate.Notif = mosaikChildRestrict.Notif;
            await _context.SaveChangesAsync();
        }

        public async Task<String> DeleteRestrictedLink(string Email, string Link)
        {
            var children = await _context.MosaikChildren.ToListAsync();
            foreach (var item in children)
            {
                if (item.Email == Email)
                {
                    var childRestricts = await _context.MosaikChildRestricts.ToListAsync();
                    foreach (var childRestrict in childRestricts)
                    {
                        if (childRestrict.Link == Link)
                        {
                            _context.MosaikChildRestricts.Remove(childRestrict);
                            await _context.SaveChangesAsync();
                            return "success";
                        }
                    }
                    return "failed";
                }
            }
            return "failed";
        }

        public async Task<LinkAndNotif> RestrictedLinkData(string Email)
        {
            var children = await _context.MosaikChildren.ToListAsync();
            List<string> Links = new List<string>();
            List<bool> Notifs = new List<bool>();
            foreach (var item in children)
            {
                if (item.Email == Email)
                {
                    var restricts = await _context.MosaikChildRestricts.ToListAsync();
                    foreach (var restrict in restricts)
                    {
                        if (restrict.ChildID == item.MosaikChildID)
                        {
                            Links.Add(restrict.Link);
                            Notifs.Add(restrict.Notif);
                        }
                    }
                }
            }
            LinkAndNotif linkAndNotif = new()
            {
                Links = Links,
                Notifs = Notifs
            };
            return linkAndNotif;
        }
        public async Task<int> GetTotalRestrictedLinkData(string Email)
        {
            var users = await _context.MosaikChildren.ToListAsync();
            foreach (var user in users)
            {
                if (user.Email == Email)
                {
                    int count = _context.MosaikChildRestricts.Where(o => o.ChildID == user.MosaikChildID).Count();
                    return count;
                }
            }
            return -1;
        }
    }
}
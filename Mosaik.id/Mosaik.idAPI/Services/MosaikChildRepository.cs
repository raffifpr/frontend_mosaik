using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Data;
using Mosaik.idAPI.Services;
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
            
            foreach (var item in list)
            {
                if (item.Email == Email)
                {
                    return item;
                }
            }
            return null;
        }
        public async Task<string> AuthorizeRequest(string Email, string EmailSupervisor, string StatusAccept)
        {
            MosaikChild mosaikChild = await GetChildAccount(Email);
            MosaikParentRepository mosaikParentRepository = new MosaikParentRepository(_context);
            MosaikParent mosaikParent = await mosaikParentRepository.Get(EmailSupervisor);
            if (mosaikChild == null || mosaikParent == null)
            {
                return "failed";
            }
            MosaikParentChild mosaikParentChild = await _context.MosaikParentsChildren.Where(o => o.childID == mosaikChild.MosaikChildID && o.parentID == mosaikParent.MosaikParentID).FirstAsync();
            if (mosaikParentChild == null)
            {
                return "failed";
            }
            bool authorized = StatusAccept == "accept" ? true : false; 
            mosaikParentChild.Authorized = authorized;
            await _context.SaveChangesAsync();
            return "success";
        }
    }
}
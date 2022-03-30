using Microsoft.EntityFrameworkCore;
using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Data
{
    public class DataContext: DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }
 
        public DbSet<MosaikHistory> MosaikHistories { get; init; }

        public DbSet<MosaikChild> MosaikChildren {get; init; }
        public DbSet<MosaikParent> MosaikParents {get; init; }
        public DbSet<MosaikParentChild> MosaikParentsChildren {get; init; }
        public DbSet<MosaikChildRestrict> MosaikChildRestricts {get; init;}
        public DbSet<MosaikDateHistory> MosaikDateHistories {get; init;}
    }
}
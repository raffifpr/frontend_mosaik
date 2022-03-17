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

        public DbSet<MosaikItem> MosaikItems {get; init; }
    }
}
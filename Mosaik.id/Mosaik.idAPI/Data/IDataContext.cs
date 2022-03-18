using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Data
{
    public interface IDataContext
    {
        DbSet<MosaikHistory> MosaikHistories { get; init; }
        DbSet<MosaikItem> MosaikItems {get; init; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
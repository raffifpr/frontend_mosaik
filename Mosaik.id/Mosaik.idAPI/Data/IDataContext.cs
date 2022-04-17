using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Data
{
    public interface IDataContext
    {
        DbSet<MosaikHistory> MosaikHistories { get; init; }
        DbSet<MosaikChild> MosaikChildren {get; init; }
        DbSet<MosaikParent> MosaikParents {get; init; }
        DbSet<MosaikParentChild> MosaikParentsChildren {get; init; }
        DbSet<MosaikChildRestrict> MosaikChildRestricts {get; init;}
        DbSet<MosaikDateHistory> MosaikDateHistories {get; init;}
        DbSet<MosaikUser> MosaikUsers {get; init;}
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
using System.Collections.Generic;
using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikHistoryRepository
    {
        Task<IEnumerable<MosaikHistory>> getAll();

        Task InsertHistory(MosaikHistory mosaikHistory);
    }
}

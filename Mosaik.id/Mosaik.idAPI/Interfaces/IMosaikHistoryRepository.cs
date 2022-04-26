using System.Collections.Generic;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Dtos;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikHistoryRepository
    {
        Task<IEnumerable<MosaikHistory>> getAll();
        Task<String> InsertHistory(string Email, string Link, string Time, string Date);
        Task<List<String>> GetListDateHistory(string Email);
        Task<int> GetTotalDateHistory(string Email);
        Task<int> GetTotalHistoryPerDate(string Email, string Date);
        Task<LinkAndTimes> GetListDateHistoryPerDate(string Email, string Date);
        Task DeleteHistory(int id);
    }
}

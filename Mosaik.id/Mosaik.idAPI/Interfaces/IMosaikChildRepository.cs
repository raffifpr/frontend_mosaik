using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikChildRepository
    {
        Task<MosaikChild> GetChildAccount(string Email);
    }
}
using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikParentRepository
    {
        Task InsertChildAccount(string Email, MosaikParentChild mosaikParentChild);
        Task<MosaikParent> Get(string Email);
        Task<IEnumerable<MosaikParentChild>> GetMosaikChildrenParent();
        Task DeleteChildAccount(string Email);
    }
}
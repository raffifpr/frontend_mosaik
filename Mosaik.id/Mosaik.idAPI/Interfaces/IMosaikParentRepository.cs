using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikParentRepository
    {
        Task InsertChildAccount(MosaikParentChild mosaikParentChild);
        Task<IEnumerable<MosaikParentChild>> GetMosaikChildrenParent();
        Task DeleteChildAccount(string Email);
    }
}
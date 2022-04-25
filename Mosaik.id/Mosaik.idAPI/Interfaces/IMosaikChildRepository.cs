using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikChildRepository
    {
        Task<MosaikChild> GetChildAccount(string Email);

        Task<String> AuthorizeRequest(string Email, string EmailSupervisor, string StatusAccept);
    }
}
using System.Collections.Generic;
using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikRepository
    {
        // bool IsAccountValid(MosaikItem mosaikItem);

        Task<IEnumerable<MosaikItem>> getAll();

        Task InsertAccount(MosaikItem mosaikItem);

        Task<bool> AuthenticateAccount(string Email, string password);

        // bool CheckUsernameUsed(string Email);

        // bool DoesItemExist(int ID);
    }
}

using System.Collections.Generic;
using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikRepository
    {
        // bool IsAccountValid(MosaikItem mosaikItem);

        Task<IEnumerable<MosaikParent>> getAll();

        Task InsertAccount(MosaikParent mosaikParent);

        Task InsertAccount(MosaikChild mosaikChild);

        Task<bool> AuthenticateAccount(string Email, string password);

        // bool CheckUsernameUsed(string Email);

        // bool DoesItemExist(int ID);
    }
}

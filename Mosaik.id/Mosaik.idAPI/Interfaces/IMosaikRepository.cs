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

        Task<Tuple<int, String, String>> AuthenticateAccount(string Email, string password);
        Task<Tuple<String, String>[]> GetSupervisedRequests(int mosaikChildID);
        Task<Tuple<String, String>[]> GetSupervisorAccounts(int mosaikParentID);

        // bool CheckUsernameUsed(string Email);

        // bool DoesItemExist(int ID);
    }
}

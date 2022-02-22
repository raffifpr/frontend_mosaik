using System.Collections.Generic;
using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikRepository
    {
        bool IsAccountValid(MosaikItem mosaikItem);

        IEnumerable<MosaikItem> All { get; }

        void InsertAccount(MosaikItem mosaikItem);

        bool CheckUsernameUsed(string Email);
    }
}

using Mosaik.idAPI.Models;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikRestrictRepository
    {
        Task InsertRestrictedLink(MosaikChildRestrict mosaikChildRestrict);

        Task<IEnumerable<MosaikChildRestrict>> GetMosaikChildRestricts();

        Task DisableNotif(MosaikChildRestrict mosaikChildRestrict);

        Task DeleteRestrictedLink(int MosaikChildRestrictID);
    }
}
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Dtos;

namespace Mosaik.idAPI.Interfaces
{
    public interface IMosaikRestrictRepository
    {
        Task<String> InsertRestrictedLink(string Email, string Link);

        Task<IEnumerable<MosaikChildRestrict>> GetMosaikChildRestricts();

        Task DisableNotif(MosaikChildRestrict mosaikChildRestrict);

        Task<string> DeleteRestrictedLink(string Email, string Link);
        Task<LinkAndNotif> RestrictedLinkData(string Email);
        Task<int> GetTotalRestrictedLinkData(string Email);
    }
}
using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPublicUserAnnounceService
    {
        Task<Pagination<AnnounceForUserDto>> GetAnnounceByUserIdAsync(AnnounceParams queryParams, int userId);
        Task<Pagination<HomeAnnounceForUserDto>> GetHomeAnnounceByUserIdAsync(HomeAnnounceParams queryParams, int userId);
        Task<Pagination<VehicleAnnounceForUserDto>> GetVehicleAnnounceByUserIdAsync(VehicleAnnounceParams queryParams, int userId);
        
    }
}
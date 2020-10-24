using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IKiosksService
    {
         Task<KiosksForReturnDto> KiosksAsync(int screenId);
         Task<KiosksForReturnDto> KiosksBySubscreenId(int subscreenId);
         Task<AnnounceForKiosksToReturnDto> GetAnnounceByIdAsync(int announceId);
         Task<ScreenForKiosksToReturnDto> GetScreenByIdAsync(int screenId);
         Task<HomeAnnounceForKiosksForReturnDto> GetHomeAnnounceByIdAsync(int announceId);
         Task<VehicleAnnounceForKiosksToReturnDto> GetVehicleAnnounceByIdAsync(int announceId);
         Task<NewsForKiosksToReturnDto> GetNewsById(int newsId);
         Task<FoodMenuForKiosksToReturnDto> GetFoodMenuById(int foodMenuId);
    }
}
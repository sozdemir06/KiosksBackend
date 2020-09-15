using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.AnnounceSpecification;
using DataAccess.EntitySpecification.FoodMenuSpecification;
using DataAccess.EntitySpecification.HomeAnnounceSpecification;
using DataAccess.EntitySpecification.NewsSpecification;
using DataAccess.EntitySpecification.ScreenSpecification;
using DataAccess.EntitySpecification.VehicleAnnounceSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class KiosksManager : IKiosksService
    {
        private readonly IMapper mapper;
        private readonly IScreenDal screenDal;
        private readonly ILiveTvBroadCastDal liveTvBroadCastDal;
        private readonly ISubSCreenDal subSCreenDal;
        private readonly IAnnounceDal announceDal;
        private readonly IHomeAnnounceDal homeAnnounceDal;
        private readonly IVehicleAnnounceDal vehicleAnnounceDal;
        private readonly INewsDal newsDal;
        private readonly IFoodMenuDal foodMenuDal;
        public KiosksManager(IMapper mapper, IScreenDal screenDal,ILiveTvBroadCastDal liveTvBroadCastDal, 
            INewsDal newsDal,
            IFoodMenuDal foodMenuDal,
            IHomeAnnounceDal homeAnnounceDal, IVehicleAnnounceDal vehicleAnnounceDal,
            ISubSCreenDal subSCreenDal, IAnnounceDal announceDal)
        {
            this.vehicleAnnounceDal = vehicleAnnounceDal;
            this.homeAnnounceDal = homeAnnounceDal;
            this.announceDal = announceDal;
            this.subSCreenDal = subSCreenDal;
            this.screenDal = screenDal;
            this.liveTvBroadCastDal = liveTvBroadCastDal;
            this.mapper = mapper;
            this.newsDal = newsDal;
            this.foodMenuDal = foodMenuDal;

        }
        public async Task<KiosksForReturnDto> KiosksAsync(int screenId)
        {
            var screenSpec = new ScreenWithSubScreenSpecification(screenId);
            var screenFromRepo = await screenDal.GetEntityWithSpecAsync(screenSpec);
            var announceFromRepo = await announceDal.GetAnnounceForKiosksByScreenIdAsync(screenId);
            var homeAnnounceFromRepo = await homeAnnounceDal.GetHomeAnnouncesForKiosksByScreenIdAsync(screenId);
            var vehicleAnnounceFromRepo=await vehicleAnnounceDal.GetVehicleAnnouncesForKiosksByScreenIdAsync(screenId);
            var newsFromRepo=await newsDal.GetNewsForKiosksByScreenIdAsync(screenId);
            var foodsMenuFromRepo=await foodMenuDal.GetFoodsMenuForKiosksByScreenIdAsync(screenId);
            var liveTvBroadCasts=await liveTvBroadCastDal.GetLiveTvBroadCastForKiosksByScreenIdAsync(screenId);

            return new KiosksForReturnDto()
            {
                Screen = mapper.Map<Screen, ScreenForKiosksToReturnDto>(screenFromRepo),
               
                Announces = mapper.Map<List<Announce>, List<AnnounceForKiosksToReturnDto>>(announceFromRepo),
                HomeAnnounces = mapper.Map<List<HomeAnnounce>,List<HomeAnnounceForKiosksForReturnDto>>(homeAnnounceFromRepo),
                VehicleAnnounces = mapper.Map<List<VehicleAnnounce>,List<VehicleAnnounceForKiosksToReturnDto>>(vehicleAnnounceFromRepo),
                News = mapper.Map<List<News>,List<NewsForKiosksToReturnDto>>(newsFromRepo),
                FoodsMenu = mapper.Map<List<FoodMenu>,List<FoodMenuForKiosksToReturnDto>>(foodsMenuFromRepo),
                LiveTvBroadCasts=mapper.Map<List<LiveTvBroadCast>,List<LiveTvBroadCastForKiosksToReturnDto>>(liveTvBroadCasts)
       
            };

        }
    }
}
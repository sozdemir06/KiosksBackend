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
        public KiosksManager(IMapper mapper, IScreenDal screenDal, ILiveTvBroadCastDal liveTvBroadCastDal,
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

        public async Task<AnnounceForKiosksToReturnDto> GetAnnounceByIdAsync(int announceId)
        {
            var spec = new AnnounceWithUserSpecification(announceId);
            var announce = await announceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<Announce, AnnounceForKiosksToReturnDto>(announce);
        }

        public async Task<FoodMenuForKiosksToReturnDto> GetFoodMenuById(int foodMenuId)
        {
            var spec = new FoodMenuWithUserSpecification(foodMenuId);
            var foodMenu = await foodMenuDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<FoodMenu, FoodMenuForKiosksToReturnDto>(foodMenu);
        }

        public async Task<HomeAnnounceForKiosksForReturnDto> GetHomeAnnounceByIdAsync(int announceId)
        {
            var spec = new HomeAnnounceWithPhotoAndUserSpecification(announceId);
            var homeannounce = await homeAnnounceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<HomeAnnounce, HomeAnnounceForKiosksForReturnDto>(homeannounce);
        }

        public async Task<NewsForKiosksToReturnDto> GetNewsById(int newsId)
        {
            var spec = new NewsWithUserSpecification(newsId);
            var news = await newsDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<News, NewsForKiosksToReturnDto>(news);
        }

        public async Task<ScreenForKiosksToReturnDto> GetScreenByIdAsync(int screenId)
        {
            var spec=new ScreenWithSubScreenSpecification(screenId);
            var screen=await screenDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<Screen,ScreenForKiosksToReturnDto>(screen);
        }

        public async Task<VehicleAnnounceForKiosksToReturnDto> GetVehicleAnnounceByIdAsync(int announceId)
        {
            var spec = new VehicleAnnounceWithPagingSpecification(announceId);
            var vehicleAnnounce = await vehicleAnnounceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<VehicleAnnounce, VehicleAnnounceForKiosksToReturnDto>(vehicleAnnounce);
        }

        public async Task<KiosksForReturnDto> KiosksAsync(int screenId)
        {
            var screenSpec = new ScreenWithSubScreenSpecification(screenId);
            var screenFromRepo = await screenDal.GetEntityWithSpecAsync(screenSpec);
            var announceFromRepo = await announceDal.GetAnnounceForKiosksByScreenIdAsync(screenId);
            var homeAnnounceFromRepo = await homeAnnounceDal.GetHomeAnnouncesForKiosksByScreenIdAsync(screenId);
            var vehicleAnnounceFromRepo = await vehicleAnnounceDal.GetVehicleAnnouncesForKiosksByScreenIdAsync(screenId);
            var newsFromRepo = await newsDal.GetNewsForKiosksByScreenIdAsync(screenId);
            var foodsMenuFromRepo = await foodMenuDal.GetFoodsMenuForKiosksByScreenIdAsync(screenId);
            var liveTvBroadCasts = await liveTvBroadCastDal.GetLiveTvBroadCastForKiosksByScreenIdAsync(screenId);

            return new KiosksForReturnDto()
            {
                Screen = mapper.Map<Screen, ScreenForKiosksToReturnDto>(screenFromRepo),
                Announces = mapper.Map<List<Announce>, List<AnnounceForKiosksToReturnDto>>(announceFromRepo),
                HomeAnnounces = mapper.Map<List<HomeAnnounce>, List<HomeAnnounceForKiosksForReturnDto>>(homeAnnounceFromRepo),
                VehicleAnnounces = mapper.Map<List<VehicleAnnounce>, List<VehicleAnnounceForKiosksToReturnDto>>(vehicleAnnounceFromRepo),
                News = mapper.Map<List<News>, List<NewsForKiosksToReturnDto>>(newsFromRepo),
                FoodsMenu = mapper.Map<List<FoodMenu>, List<FoodMenuForKiosksToReturnDto>>(foodsMenuFromRepo),
                LiveTvBroadCasts = mapper.Map<List<LiveTvBroadCast>, List<LiveTvBroadCastForKiosksToReturnDto>>(liveTvBroadCasts)

            };

        }

        public async Task<KiosksForReturnDto> KiosksBySubscreenId(int subscreenId)
        {

            var announceFromRepo = await announceDal.GetAnnounceForKiosksBySubScreenIdAsync(subscreenId);
            var homeAnnounceFromRepo = await homeAnnounceDal.GetHomeAnnouncesForKiosksBySubScreenIdAsync(subscreenId);
            var vehicleAnnounceFromRepo = await vehicleAnnounceDal.GetVehicleAnnouncesForKiosksBySubScreenIdAsync(subscreenId);
            var newsFromRepo = await newsDal.GetNewsForKiosksBySubScreenIdAsync(subscreenId);
            var foodsMenuFromRepo = await foodMenuDal.GetFoodsMenuForKiosksByScreenIdAsync(subscreenId);
            var liveTvBroadCasts = await liveTvBroadCastDal.GetLiveTvBroadCastForKiosksBySubScreenIdAsync(subscreenId);

            return new KiosksForReturnDto()
            {
                Announces = mapper.Map<List<Announce>, List<AnnounceForKiosksToReturnDto>>(announceFromRepo),
                HomeAnnounces = mapper.Map<List<HomeAnnounce>, List<HomeAnnounceForKiosksForReturnDto>>(homeAnnounceFromRepo),
                VehicleAnnounces = mapper.Map<List<VehicleAnnounce>, List<VehicleAnnounceForKiosksToReturnDto>>(vehicleAnnounceFromRepo),
                News = mapper.Map<List<News>, List<NewsForKiosksToReturnDto>>(newsFromRepo),
                FoodsMenu = mapper.Map<List<FoodMenu>, List<FoodMenuForKiosksToReturnDto>>(foodsMenuFromRepo),
                LiveTvBroadCasts = mapper.Map<List<LiveTvBroadCast>, List<LiveTvBroadCastForKiosksToReturnDto>>(liveTvBroadCasts)

            };
        }
    }
}
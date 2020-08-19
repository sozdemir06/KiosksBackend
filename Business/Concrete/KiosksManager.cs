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
        private readonly ISubSCreenDal subSCreenDal;
        private readonly IAnnounceDal announceDal;
        private readonly IHomeAnnounceDal homeAnnounceDal;
        private readonly IVehicleAnnounceDal vehicleAnnounceDal;
        private readonly INewsDal newsDal;
        private readonly IFoodMenuDal foodMenuDal;
        public KiosksManager(IMapper mapper, IScreenDal screenDal, INewsDal newsDal, IFoodMenuDal foodMenuDal,
            IHomeAnnounceDal homeAnnounceDal, IVehicleAnnounceDal vehicleAnnounceDal,
            ISubSCreenDal subSCreenDal, IAnnounceDal announceDal)
        {
            this.vehicleAnnounceDal = vehicleAnnounceDal;
            this.homeAnnounceDal = homeAnnounceDal;
            this.announceDal = announceDal;
            this.subSCreenDal = subSCreenDal;
            this.screenDal = screenDal;
            this.mapper = mapper;
            this.newsDal = newsDal;
            this.foodMenuDal = foodMenuDal;

        }
        public async Task<KiosksForReturnDto> KiosksAsync(int screenId)
        {
            var screenSpec = new ScreenWithSubScreenSpecification(screenId);
            var screenFromRepo = await screenDal.GetEntityWithSpecAsync(screenSpec);

            var subScreenFromRepo = await subSCreenDal.GetListAsync(x => x.ScreenId == screenId);

            var announceSpec = new AnnounceWithDetailSpecification();
            var announceFromRepo = await announceDal.ListEntityWithSpecAsync(announceSpec);

            var homeAnnounceSpec = new HomeAnnounceDetailSpecification();
            var homeAnnounceFromRepo = await homeAnnounceDal.ListEntityWithSpecAsync(homeAnnounceSpec);

            var vehicleAnnouncespec=new VehicleAnnounceDetailSpecification();
            var vehicleAnnounceFromRepo=await vehicleAnnounceDal.ListEntityWithSpecAsync(vehicleAnnouncespec);

            var newsSpec=new NewsWithDetailSpecification();
            var newsFromRepo=await newsDal.ListEntityWithSpecAsync(newsSpec);

            var foodsMenuSpec=new FoodMenuWithDetailSpecification();
            var foodsMenuFromRepo=await foodMenuDal.ListEntityWithSpecAsync(foodsMenuSpec);

            return new KiosksForReturnDto()
            {
                Screen = mapper.Map<Screen, ScreenForReturnDto>(screenFromRepo),
                SubScreens = mapper.Map<List<SubScreen>, List<SubScreenForReturnDto>>(subScreenFromRepo),
                Announces = mapper.Map<List<Announce>, List<AnnounceForDetailDto>>(announceFromRepo),
                HomeAnnounces = mapper.Map<List<HomeAnnounce>,List<HomeAnnounceForDetailDto>>(homeAnnounceFromRepo),
                Vehicleannounces = mapper.Map<List<VehicleAnnounce>,List<VehicleAnnounceForDetailDto>>(vehicleAnnounceFromRepo),
                News = mapper.Map<List<News>,List<NewsForDetailDto>>(newsFromRepo),
                FoodsMenu = mapper.Map<List<FoodMenu>,List<FoodMenuForDetailDto>>(foodsMenuFromRepo),
        
                
            };

        }
    }
}
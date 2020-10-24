using System.Linq;
using AutoMapper;
using Business.Abstract;
using Core.Entities;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace Business.MappingProfile
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, UserRoleForListDto>();
            CreateMap<OnlineScreen, OnlineScreenForReturnDto>();
            CreateMap<User, UserForListDto>()
           .ForMember(x => x.Avatar, o => o.MapFrom(z => z.UserPhotos.FirstOrDefault(x => x.IsConfirm && !x.UnConfirm).FullPath));
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserPhoto, UserPhotoForReturnDto>();
            CreateMap<UserPhotoForCreationDto, UserPhoto>();


            CreateMap<Campus, CampusForReturnDto>();
            CreateMap<CampuseForCreationDto, Campus>();

            CreateMap<Department, DepartmentForReturnDto>();
            CreateMap<DepartmentForCreationDto, Department>();

            CreateMap<Degree, DegreeForReturnDto>();
            CreateMap<DegreeForCreationDto, Degree>();

            CreateMap<Role, RoleForListDto>();
            CreateMap<RoleCategory, RoleCategoryForListDto>();
            CreateMap<RoleForCreationAndUpdateDto, Role>();
            CreateMap<RoleCategoryForCreationAndUpdateDto, RoleCategory>();

            CreateMap<NumberOfRoom, NumberOfRoomForReturnDto>();
            CreateMap<NumberOfRoomForCreateOrUpdateDto, NumberOfRoom>();

            CreateMap<BuildingAge, BuildingAgeForReturnDto>();
            CreateMap<BuildingAgeForCretationDto, BuildingAge>();

            CreateMap<FlatOfHome, FlatOfHomeForReturnDto>();
            CreateMap<FlatOfHomeForCreationDto, FlatOfHome>();

            CreateMap<HeatingType, HeatingTypeForReturnDto>();
            CreateMap<HeatingTypeForCreationDto, HeatingType>();

            CreateMap<VehicleCategory, VehicleCategoryForReturnDto>();
            CreateMap<VehicleCategoryForCreationDto, VehicleCategory>();

            CreateMap<VehicleBrand, VehicleBrandForReturnDto>();
            CreateMap<VehicleBrandForCreationDto, VehicleBrand>();

            CreateMap<VehicleModel, VehicleModelForReturnDto>();
            CreateMap<VehicleModelForCreationDto, VehicleModel>();

            CreateMap<VehicleFuelType, VehicleFuelTypeForReturnDto>();
            CreateMap<VehicleFuelTypeForCreationDto, VehicleFuelType>();

            CreateMap<VehicleGearType, VehicleGearTypeForReturnDto>();
            CreateMap<VehicleGearTypeForCreationDto, VehicleGearType>();

            CreateMap<VehicleEngineSize, VehicleEngineSizeForReturnDto>();
            CreateMap<VehicleEngineSizeForCreationDto, VehicleEngineSize>();

            CreateMap<Screen, ScreenForReturnDto>();
            CreateMap<Screen, ScreenForKiosksToReturnDto>();
            CreateMap<ScreenForCreationDto, Screen>();

            CreateMap<SubScreen, SubScreenForReturnDto>();
            CreateMap<SubScreenForCreationDto, SubScreen>();

            CreateMap<HomeAnnounce, HomeAnnounceForReturnDto>();
            CreateMap<HomeAnnounce, HomeAnnounceForKiosksForReturnDto>()
                    .ForMember(x => x.HomeAnnouncePhotos, o => o.MapFrom(z => z.HomeAnnouncePhotos.Where(t => t.IsConfirm == true)));
            CreateMap<HomeAnnounce, HomeAnnounceForPublicDto>()
                   .ForMember(x => x.HomeAnnouncePhotos, o => o.MapFrom(z => z.HomeAnnouncePhotos.Where(t => t.IsConfirm == true)))
                    .ForMember(x => x.PhotoUrl, o => o.MapFrom(z => z.HomeAnnouncePhotos.FirstOrDefault(x => x.IsConfirm).FullPath));
            CreateMap<HomeAnnounce, HomeAnnounceForUserDto>()
                  .ForMember(x => x.PhotoUrl, o => o.MapFrom(z => z.HomeAnnouncePhotos.FirstOrDefault(x => x.IsConfirm).FullPath));
            CreateMap<HomeAnnounceForCreationDto, HomeAnnounce>();

            CreateMap<HomeAnnounce, HomeAnnounceForDetailDto>();

            CreateMap<HomeAnnounceSubScreen, HomeAnnounceSubScreenForReturnDto>();
            CreateMap<HomeAnnounceSubScreenForCreationDto, HomeAnnounceSubScreen>();

            CreateMap<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>();
            CreateMap<HomeAnnouncePhotoForCreationDto, HomeAnnouncePhoto>();

            CreateMap<VehicleAnnounce, VehicleAnnounceForReturnDto>()
                    .ForMember(x => x.VehicleCategoryName, o => o.MapFrom(z => z.VehicleCategory.CategoryName))
                    .ForMember(x => x.VehicleBrandName, o => o.MapFrom(z => z.VehicleBrand.BrandName))
                    .ForMember(x => x.VehicleModelName, o => o.MapFrom(z => z.VehicleModel.VehicleModelName));
            CreateMap<VehicleAnnounce, VehicleAnnounceForKiosksToReturnDto>()
                 .ForMember(x => x.VehicleAnnouncePhotos, o => o.MapFrom(z => z.VehicleAnnouncePhotos.Where(t => t.IsConfirm == true)))
                  .ForMember(x => x.VehicleCategoryName, o => o.MapFrom(z => z.VehicleCategory.CategoryName))
                    .ForMember(x => x.VehicleBrandName, o => o.MapFrom(z => z.VehicleBrand.BrandName))
                    .ForMember(x => x.VehicleModelName, o => o.MapFrom(z => z.VehicleModel.VehicleModelName));
            CreateMap<VehicleAnnounce, VehicleAnnounceForPublicDto>()
               .ForMember(x => x.VehicleAnnouncePhotos, o => o.MapFrom(z => z.VehicleAnnouncePhotos.Where(t => t.IsConfirm == true)))
                .ForMember(x => x.PhotoUrl, o => o.MapFrom(z => z.VehicleAnnouncePhotos.FirstOrDefault(x => x.IsConfirm).FullPath))
                .ForMember(x => x.VehicleCategoryName, o => o.MapFrom(z => z.VehicleCategory.CategoryName))
                  .ForMember(x => x.VehicleBrandName, o => o.MapFrom(z => z.VehicleBrand.BrandName))
                  .ForMember(x => x.VehicleModelName, o => o.MapFrom(z => z.VehicleModel.VehicleModelName));
            CreateMap<VehicleAnnounce, VehicleAnnounceForUserDto>()
               .ForMember(x => x.PhotoUrl, o => o.MapFrom(z => z.VehicleAnnouncePhotos.FirstOrDefault(x => x.IsConfirm).FullPath))
               .ForMember(x => x.VehicleCategoryName, o => o.MapFrom(z => z.VehicleCategory.CategoryName))
                 .ForMember(x => x.VehicleBrandName, o => o.MapFrom(z => z.VehicleBrand.BrandName))
                 .ForMember(x => x.VehicleModelName, o => o.MapFrom(z => z.VehicleModel.VehicleModelName));
            CreateMap<VehicleAnnounce, VehicleAnnounceForDetailDto>()
                 .ForMember(x => x.VehicleCategoryName, o => o.MapFrom(z => z.VehicleCategory.CategoryName))
                    .ForMember(x => x.VehicleBrandName, o => o.MapFrom(z => z.VehicleBrand.BrandName))
                    .ForMember(x => x.VehicleModelName, o => o.MapFrom(z => z.VehicleModel.VehicleModelName));


            CreateMap<VehicleAnnounceForCreationDto, VehicleAnnounce>();

            CreateMap<VehicleAnnounceSubScreen, VehicleAnnounceSubScreenForReturnDto>();
            CreateMap<VehicleAnnounceSubScreenForCreationDto, VehicleAnnounceSubScreen>();

            CreateMap<VehicleAnnouncePhoto, VehicleAnnouncePhotoForReturnDto>();
            CreateMap<VehicleAnnouncePhotoForCreationDto, VehicleAnnouncePhoto>();

            CreateMap<Announce, AnnounceForReturnDto>();
            CreateMap<Announce, AnnounceForKiosksToReturnDto>()
                     .ForMember(x => x.AnnouncePhotos, o => o.MapFrom(z => z.AnnouncePhotos.Where(t => t.IsConfirm == true)));
            CreateMap<Announce, AnnounceForPublicDto>()
                   .ForMember(x => x.AnnouncePhotos, o => o.MapFrom(z => z.AnnouncePhotos.Where(t => t.IsConfirm == true)))
                   .ForMember(x => x.PhotoUrl, o => o.MapFrom(z => z.AnnouncePhotos.FirstOrDefault(x => x.IsConfirm).FullPath));
            CreateMap<Announce, AnnounceForUserDto>()
                 .ForMember(x => x.PhotoUrl, o => o.MapFrom(z => z.AnnouncePhotos.FirstOrDefault(x => x.IsConfirm).FullPath));
            CreateMap<AnnounceForCreationDto, Announce>();
            CreateMap<Announce, AnnounceForDetailDto>();

            CreateMap<AnnounceSubScreen, AnnounceSubScreenForReturnDto>();
            CreateMap<AnnounceSubScreenForCreationDto, AnnounceSubScreen>();

            CreateMap<AnnouncePhoto, AnnouncePhotoForReturnDto>();
            CreateMap<AnnouncePhotoForCretionDto, AnnouncePhoto>();

            CreateMap<AnnounceContentType, AnnounceContentTypeForReturnDto>();
            CreateMap<AnnounceContentTypeForCreationDto, AnnounceContentType>();

            CreateMap<News, NewsForReturnDto>();
            CreateMap<News, NewsForKiosksToReturnDto>()
                         .ForMember(x => x.NewsPhotos, o => o.MapFrom(z => z.NewsPhotos.Where(t => t.IsConfirm == true)));
            CreateMap<News, NewsForPublicDto>()
                         .ForMember(x => x.NewsPhotos, o => o.MapFrom(z => z.NewsPhotos.Where(t => t.IsConfirm == true)))
                          .ForMember(x => x.PhotoUrl, o => o.MapFrom(z => z.NewsPhotos.FirstOrDefault(x => x.IsConfirm).FullPath));
            CreateMap<NewsForCreationDto, News>();

            CreateMap<News, NewsForDetailDto>();

            CreateMap<NewsSubScreen, NewsSubScreenForReturnDto>();
            CreateMap<NewsSubScreenForCreationDto, NewsSubScreen>();

            CreateMap<NewsPhoto, NewsPhotoForReturnDto>();
            CreateMap<NewsPhotoForCreationDto, NewsPhoto>();

            CreateMap<FoodMenu, FoodMenuForReturnDto>();
            CreateMap<FoodMenu, FoodMenuForKiosksToReturnDto>()
                 .ForMember(x => x.FoodMenuPhotos, o => o.MapFrom(z => z.FoodMenuPhotos.Where(t => t.IsConfirm == true)));
            CreateMap<FoodMenu, FoodMenuForPublicDto>()
               .ForMember(x => x.FoodMenuPhotos, o => o.MapFrom(z => z.FoodMenuPhotos.Where(t => t.IsConfirm == true)))
                .ForMember(x => x.PhotoUrl, o => o.MapFrom(z => z.FoodMenuPhotos.FirstOrDefault(x => x.IsConfirm).FullPath));
            CreateMap<FoodMenuForCreationDto, FoodMenu>();

            CreateMap<FoodMenu, FoodMenuForDetailDto>();

            CreateMap<FoodMenuSubscreen, FoodMenuSubScreenForReturnDto>();
            CreateMap<FoodMenuSubScreenForCreationDto, FoodMenuSubscreen>();

            CreateMap<FoodMenuPhoto, FoodMenuPhotoForReturnDto>();
            CreateMap<FoodMenuPhotoForCreationDto, FoodMenuPhoto>();

            CreateMap<FoodMenuBgPhoto, FoodMenuBgPhotoForReturnDto>();
            CreateMap<FoodMenuBgPhotoForCreationDto, FoodMenuBgPhoto>();

            CreateMap<ScreenHeader, ScreenHeaderForReturnDto>();
            CreateMap<ScreenHeaderForCreationDto, ScreenHeader>();

            CreateMap<ScreenFooter, ScreenFooterForReturnDto>();
            CreateMap<ScreenFooterForCreationDto, ScreenFooter>();

            CreateMap<ScreenHeaderPhoto, ScreenHeaderPhotoForReturnDto>();
            CreateMap<ScreenHeaderPhotoForCreationDto, ScreenHeaderPhoto>();

            CreateMap<City, CityForReturnDto>();
            CreateMap<CityForCreationDto, City>();

            CreateMap<Currency, CurrencyForReturnDto>();
            CreateMap<CurrencyForCreationDto, Currency>();

            CreateMap<WheatherForeCastHttpResponseDto, WheatherForeCastForReturnDto>()
                    .ForMember(x => x.CityName, o => o.MapFrom(z => z.name))
                    .ForMember(x => x.Temp, o => o.MapFrom(z => z.main.temp))
                    .ForMember(x => x.Icon, o => o.MapFrom(z => z.weather.Select(x => x.icon).FirstOrDefault()))
                    .ForMember(x => x.Pressure, o => o.MapFrom(z => z.main.pressure))
                    .ForMember(x => x.Humidity, o => o.MapFrom(z => z.main.humidity))
                    .ForMember(x => x.TempMax, o => o.MapFrom(z => z.main.temp_max))
                    .ForMember(x => x.TempMin, o => o.MapFrom(z => z.main.temp_min))
                    .ForMember(x => x.WheatherImage, o => o.MapFrom<WheatherImageUrlResolver>());


            CreateMap<LiveTvBroadCast, LiveTvBroadCastForReturnDto>();
            CreateMap<LiveTvBroadCastForCreationDto, LiveTvBroadCast>();

            CreateMap<LiveTvBroadCast, LiveTvBroadCastForDetailDto>();
            CreateMap<LiveTvBroadCast, LiveTvBroadCastForKiosksToReturnDto>();

            CreateMap<LiveTvList, LiveTvListForReturnDto>();
            CreateMap<LiveTvListForCreationDto, LiveTvList>();

            CreateMap<LiveTvBroadCastSubScreen, LiveTvBroadCastSubScreenForReturnDto>();
            CreateMap<LiveTvBroadCastSubScreenForCreationDto, LiveTvBroadCastSubScreen>();

            CreateMap<NotifyGroup, NotifyGroupForReturnDto>();
            CreateMap<NotifyGroupForCreationDto, NotifyGroup>();

            CreateMap<UserNotifyGroup, UserNotifyGroupForReturnDto>()
                    .ForMember(src=>src.GroupName,o=>o.MapFrom(dest=>dest.NotifyGroup.GroupName))
                    .ForMember(src=>src.Description,o=>o.MapFrom(dest=>dest.NotifyGroup.Description));
            CreateMap<UserNotifyGroupForCreationDto, UserNotifyGroup>();

        }
    }
}
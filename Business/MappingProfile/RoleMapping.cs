using AutoMapper;
using Business.Abstract;
using Core.Entities;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.MappingProfile
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, UserRoleForListDto>();
            CreateMap<User, UserForListDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<Campus, CampusForListDto>();
            CreateMap<Department, DepartmentForListDto>();
            CreateMap<Degree, DegreeForListDto>();
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
            CreateMap<ScreenForCreationDto, Screen>();

            CreateMap<SubScreen, SubScreenForReturnDto>();
            CreateMap<SubScreenForCreationDto, SubScreen>();

            CreateMap<HomeAnnounce, HomeAnnounceForReturnDto>();
            CreateMap<HomeAnnounceForCreationDto, HomeAnnounce>();

            CreateMap<HomeAnnounce, HomeAnnounceForDetailDto>();

            CreateMap<HomeAnnounceSubScreen, HomeAnnounceSubScreenForReturnDto>();
            CreateMap<HomeAnnounceSubScreenForCreationDto, HomeAnnounceSubScreen>();

            CreateMap<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>();
            CreateMap<HomeAnnouncePhotoForCreationDto, HomeAnnouncePhoto>();

            CreateMap<VehicleAnnounce, VehicleAnnounceForReturnDto>();

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
            CreateMap<AnnounceForCreationDto, Announce>();

            CreateMap<Announce, AnnounceForDetailDto>();

            CreateMap<AnnounceSubScreen, AnnounceSubScreenForReturnDto>();
            CreateMap<AnnounceSubScreenForCreationDto, AnnounceSubScreen>();

            CreateMap<AnnouncePhoto, AnnouncePhotoForReturnDto>();
            CreateMap<AnnouncePhotoForCretionDto, AnnouncePhoto>();

            CreateMap<AnnounceContentType, AnnounceContentTypeForReturnDto>();
            CreateMap<AnnounceContentTypeForCreationDto, AnnounceContentType>();

            CreateMap<News, NewsForReturnDto>();
            CreateMap<NewsForCreationDto, News>();

            CreateMap<News, NewsForDetailDto>();

            CreateMap<NewsSubScreen, NewsSubScreenForReturnDto>();
            CreateMap<NewsSubScreenForCreationDto, NewsSubScreen>();

            CreateMap<NewsPhoto, NewsPhotoForReturnDto>();
            CreateMap<NewsPhotoForCreationDto, NewsPhoto>();

        }
    }
}
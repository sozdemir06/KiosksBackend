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

            CreateMap<HomeAnnounceSubScreen, HomeAnnounceSubScreenForReturnDto>();
            CreateMap<HomeAnnounceSubScreenForCreationDto, HomeAnnounceSubScreen>();

            CreateMap<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>();
            CreateMap<HomeAnnouncePhotoForCreationDto, HomeAnnouncePhoto>();

        }
    }
}
using AutoMapper;
using Core.Entities;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace Business.MappingProfile
{
    public class RoleMapping:Profile
    {
        public RoleMapping()
        {
            CreateMap<Role,UserRoleForListDto>();
            CreateMap<User,UserForListDto>();
            CreateMap<UserForRegisterDto,User>();
            CreateMap<Campus,CampusForListDto>();
            CreateMap<Department,DepartmentForListDto>();
            CreateMap<Degree,DegreeForListDto>();
        }
    }
}
using AutoMapper;
using Core.Entities;
using Core.Entities.Concrete;

namespace Business.MappingProfile
{
    public class RoleMapping:Profile
    {
        public RoleMapping()
        {
            CreateMap<Role,UserRoleForListDto>();
        }
    }
}
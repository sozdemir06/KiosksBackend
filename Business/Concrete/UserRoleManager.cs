using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class UserRoleManager : IUserroleService
    {
        private readonly IUserService userService;
        private readonly IUserDal userDal;
        private readonly IRoleDal roleDal;
        private readonly IUserRoleDal userRoleDal;
        private readonly IMapper mapper;
        public UserRoleManager(IUserService userService, IMapper mapper, IUserDal userDal, IRoleDal roleDal, IUserRoleDal userRoleDal)
        {
            this.mapper = mapper;

            this.userRoleDal = userRoleDal;
            this.roleDal = roleDal;
            this.userDal = userDal;
            this.userService = userService;


        }

        public async Task<RoleForListDto> AddRoleToUser(int userId, int roleId)
        {
            var userRole=await userRoleDal.GetAsync(x=>x.UserId==userId && x.RoleId==roleId);
            if(userRole!=null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }
            var role = await roleDal.GetAsync(x => x.Id == roleId);
            if (role == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { RolesListNotFound = Messages.RoleListNotFound });
            }

            var user = await userDal.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = Messages.UserNotFound });
            }

            var userrole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            await userRoleDal.Add(userrole);
            return mapper.Map<Role,RoleForListDto>(role);


        }

        public async Task<RoleForListDto> DeleteRoleFromUser(int userId,int roleId)
        {
             var role=await roleDal.GetAsync(x=>x.Id==roleId);
             if(role==null)
             {
                 throw new RestException(HttpStatusCode.BadRequest, new { RolesListNotFound = Messages.RoleListNotFound });
             }

              var user = await userDal.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = Messages.UserNotFound });
            }

            var userRole=await userRoleDal.GetAsync(x=>x.RoleId==roleId && x.UserId==userId);
            if(userRole==null)
            {
                 throw new RestException(HttpStatusCode.BadRequest, new { UserRoleNotFound = Messages.NotFound });
            }

            
             await userRoleDal.Delete(userRole);
             return mapper.Map<Role,RoleForListDto>(role);
        }

        public async Task<List<UserRoleForListDto>> GetUserRoles(int userId)
        {
            var user = await userDal.GetAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = Messages.UserNotFound });
            }

            return await userService.GetUserRoles(user);
        }
    }
}
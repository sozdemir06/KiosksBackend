using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Helpers;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.QueryParams;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.RoleSpecifications;
using DataAccess.EntitySpecification.UsersSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;
        private readonly IRoleDal roleDal;
        private readonly IMapper mapper;
        public UserManager(IUserDal userDal, IRoleDal roleDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.roleDal = roleDal;
            this.userDal = userDal;

        }
        public async Task Add(User user)
        {
            await userDal.Add(user);

        }

        public async Task<User> GetByEmail(string email)
        {
            return await userDal.GetAsync(x => x.Email == email);
        }

        public async Task<Pagination<UserForListDto>> GetUserForList(UserQueryParams userQueryParams)
        {
            var spec = new UserWithTitleAndCampusSpesification(userQueryParams);
            var users = await userDal.ListEntityWithSpecAsync(spec);
            var countSpec=new UserWithFilterForCaountSpecification(userQueryParams);
            var totalItems=await userDal.CountAsync(countSpec);

            if (users == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UsersNotFound = Messages.UserNotFound });
            }

            var data = mapper.Map<List<User>, List<UserForListDto>>(users);

            return new Pagination<UserForListDto>
             (
                 userQueryParams.PageIndex,
                 userQueryParams.PageSize,
                 totalItems,
                 data

             );

        }

        public async Task<List<UserRoleForListDto>> GetUserRoles(User user)
        {
            var spec = new UserWithRoleSpecification(user);
            var userRoles = await roleDal.ListEntityWithSpecAsync(spec);
            if (userRoles == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = Messages.UserClaimsNotFound });
            }

            var userRolesForReturn = mapper.Map<List<Role>, List<UserRoleForListDto>>(userRoles);
            return userRolesForReturn;
        }
    }
}
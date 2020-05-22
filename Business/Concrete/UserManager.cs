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
using DataAccess.EntitySpecification.RoleSpecifications;

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
            userDal.Add(user);
            var result = await userDal.SaveChangesAsync();
            if (!result)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = Messages.UserCantAdded });
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            return  await userDal.GetAsync(x => x.Email == email);  
        }

        public async Task<List<UserRoleForListDto>> GetUserRoles(User user)
        {
            var spec = new UserWithRoleSpecification(user);
            var userRoles = await roleDal.ListEntityWithSpecAsync(spec);
            if (userRoles == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = Messages.UserClaimsNotFound });
            }

            var userRolesForReturn =mapper.Map<List<Role>,List<UserRoleForListDto>>(userRoles);
            return userRolesForReturn;
        }
    }
}
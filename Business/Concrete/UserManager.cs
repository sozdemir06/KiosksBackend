using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Helpers;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Logging;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Logging.NLog.Loggers;
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
        private readonly IRoleCategoryDal roleCategoryDal;
        private readonly IAnnounceStatusCheck announceStatusCheck;
        private readonly IMapper mapper;
        private readonly IUserRoleDal userRoleDal;

        public UserManager(IUserDal userDal, IRoleDal roleDal, IRoleCategoryDal roleCategoryDal,
        IAnnounceStatusCheck announceStatusCheck,
        IMapper mapper,
        IUserRoleDal userRoleDal)
        {
            this.userRoleDal = userRoleDal;
            this.mapper = mapper;
            this.roleDal = roleDal;
            this.roleCategoryDal = roleCategoryDal;
            this.announceStatusCheck = announceStatusCheck;
            this.userDal = userDal;

        }

        [ValidationAspect(typeof(UserValidator), Priority = 2)]
        public async Task Add(User user)
        {
            await userDal.Add(user);

        }

        public async Task<User> GetByEmail(string email)
        {
            return await userDal.GetAsync(x => x.Email == email);
        }


        public async Task<UserForListDto> GetUserAsync(string email)
        {
            var spec = new UserWithCampusAndDepartmentAndDegreeSpecification(email);
            var user = await userDal.GetEntityWithSpecAsync(spec);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = Messages.UserNotFound });
            }

            var userForReturn = mapper.Map<User, UserForListDto>(user);
            return userForReturn;
        }



        public async Task<Pagination<UserForListDto>> GetUserForList(UserQueryParams userQueryParams)
        {
            var spec = new UserWithTitleAndCampusSpesification(userQueryParams);
            var users = await userDal.ListEntityWithSpecAsync(spec);
            var countSpec = new UserWithFilterForCaountSpecification(userQueryParams);
            var totalItems = await userDal.CountAsync(countSpec);

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

        [SecuredOperation("Sudo,User.Update,User.All", Priority = 1)]
        [ValidationAspect(typeof(UserValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<UserForListDto> Update(UserForRegisterDto userForRegisterDto)
        {
            var userFromRepo = await userDal.GetAsync(x => x.Email == userForRegisterDto.Email);
            if (userFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = Messages.UserNotFound });
            }

            var userForUpdate = mapper.Map(userForRegisterDto, userFromRepo);



            var userUpdate = await userDal.Update(userForUpdate);
            var spec = new UserWithCampusAndDepartmentAndDegreeSpecification(userUpdate.Email);
            var user = await userDal.GetEntityWithSpecAsync(spec);

            var chekcPublicRoles = await announceStatusCheck.CheckPublicRole(roleDal, roleCategoryDal);
            var userRolePublic = await userRoleDal.GetAsync(x => x.Role.Name.ToLower() == "public");
            if (user.IsActive)
            {

                if (userRolePublic == null)
                {
                    userRolePublic = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = chekcPublicRoles.Id
                    };
                    await userRoleDal.Add(userRolePublic);
                }
            }
            else if (!user.IsActive)
            {
                if(userRolePublic!=null)
                {
                    await userRoleDal.Delete(userRolePublic);
                }
            }
            return mapper.Map<User, UserForListDto>(user);
        }
    }
}
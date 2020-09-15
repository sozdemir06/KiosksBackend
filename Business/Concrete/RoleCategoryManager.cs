using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Core.Entities.Concrete;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class RoleCategoryManager : IRoleCategoryService
    {
        private readonly IRoleCategoryDal roleCategoryDal;
        private readonly IMapper mapper;
        public RoleCategoryManager(IRoleCategoryDal roleCategoryDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.roleCategoryDal = roleCategoryDal;

        }

        [SecuredOperation("Sudo,Roles.Create,Roles.All", Priority = 1)]
        [ValidationAspect(typeof(RoleCategoryValidator), Priority = 2)]
        public async Task<RoleCategoryForListDto> Create(RoleCategoryForCreationAndUpdateDto roleCategoryForCreationAndUpdateDto)
        {
            var checkFromRepo = await roleCategoryDal.GetAsync(x => x.Name == roleCategoryForCreationAndUpdateDto.Name);
            if (checkFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { RoleCategoryListNotFound = Messages.AlreadyExist });

            }

            var roleCategoryCreated = mapper.Map<RoleCategory>(roleCategoryForCreationAndUpdateDto);
            await roleCategoryDal.Add(roleCategoryCreated);
            return mapper.Map<RoleCategory, RoleCategoryForListDto>(roleCategoryCreated);
        }

        [SecuredOperation("Sudo,Roles.List,Roles.All", Priority = 1)]
        public async Task<List<RoleCategoryForListDto>> GetRoleCategoriesAsync()
        {
            var roleCategories = await roleCategoryDal.GetListAsync();
            if (roleCategories == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { RoleCategoryListNotFound = Messages.RoleCatgegorListListNotFound });
            }

            var roleCategoryForReturn = mapper.Map<List<RoleCategory>, List<RoleCategoryForListDto>>(roleCategories);
            return roleCategoryForReturn;
        }

        [SecuredOperation("Sudo,Roles.Update,Roles.All", Priority = 1)]
        [ValidationAspect(typeof(RoleCategoryValidator), Priority = 2)]
        public async Task<RoleCategoryForListDto> Update(RoleCategoryForCreationAndUpdateDto roleCategoryForCreationAndUpdateDto)
        {
            var roleCategory = await roleCategoryDal.GetAsync(x => x.Id == roleCategoryForCreationAndUpdateDto.Id);
            if (roleCategory == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { RoleCategoryListNotFound = Messages.NotFound });

            }

            var updatedRoleCategory = mapper.Map(roleCategoryForCreationAndUpdateDto, roleCategory);
            await roleCategoryDal.Update(updatedRoleCategory);
            return mapper.Map<RoleCategory, RoleCategoryForListDto>(updatedRoleCategory);
        }
    }
}
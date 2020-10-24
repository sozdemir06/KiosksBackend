using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Helpers;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.QueryParams;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.RolesSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleDal roleDal;
        private readonly IMapper mapper;
        public RoleManager(IRoleDal roleDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.roleDal = roleDal;

        }

        [SecuredOperation("Sudo,Roles.Create,Roles.All", Priority = 1)]
        [ValidationAspect(typeof(RoleValidator), Priority = 2)]
        public async Task<RoleForListDto> Create(RoleForCreationAndUpdateDto roleForCreationAndUpdateDto)
        {
            var checkRoleNameFormRepo = await roleDal.GetAsync(x => x.Name.ToLower() == roleForCreationAndUpdateDto.Name.ToLower());
            if (checkRoleNameFormRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { RoleAlreadyExist = Messages.RoleNameAlreadyExist });
            }

            var roleForCreate = mapper.Map<Role>(roleForCreationAndUpdateDto);
            var createdRole = await roleDal.Add(roleForCreate);
            var spec = new RolesWithRoleCategorySpecification(createdRole.Id);
            var roleToReturn = await roleDal.GetEntityWithSpecAsync(spec);



            return mapper.Map<Role, RoleForListDto>(roleToReturn);
        }

        //[SecuredOperation("Sudo,Roles.List,Roles.All", Priority = 1)]
        public async Task<Pagination<RoleForListDto>> GetRolesAsync(RoleQueryParams queryParams)
        {
            var spec = new RolesWithRoleCategorySpecification(queryParams);
            var roles = await roleDal.ListEntityWithSpecAsync(spec);
            var countSpec = new RolesWithFilterForCountSpecificatipon(queryParams);
            var totalItems = await roleDal.CountAsync(countSpec);

            if (roles == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { RolesListNotFound = Messages.RoleListNotFound });
            };

            var data = mapper.Map<List<Role>, List<RoleForListDto>>(roles);

            return new Pagination<RoleForListDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItems,
                data
            );

        }

        [SecuredOperation("Sudo,Roles.Update,Roles.All", Priority = 1)]
        [ValidationAspect(typeof(RoleValidator), Priority = 2)]
        public async Task<RoleForListDto> Update(RoleForCreationAndUpdateDto roleForCreationAndUpdateDto)
        {
            var spec = new RolesWithRoleCategorySpecification(roleForCreationAndUpdateDto.Id);
            var getRoles = await roleDal.GetEntityWithSpecAsync(spec);
            if (getRoles == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { RolesListNotFound = Messages.RoleListNotFound });
            }

            var updatedRole = mapper.Map(roleForCreationAndUpdateDto, getRoles);
            await roleDal.Update(updatedRole);

            return mapper.Map<Role, RoleForListDto>(updatedRole);

        }
    }
}
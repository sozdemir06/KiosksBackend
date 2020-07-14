using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
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

        public async Task<RoleCategoryForListDto> Create(RoleCategoryForCreationAndUpdateDto roleCategoryForCreationAndUpdateDto)
        {
               var checkFromRepo=await roleCategoryDal.GetAsync(x=>x.Name==roleCategoryForCreationAndUpdateDto.Name);
               if(checkFromRepo!=null)
               {
                    throw new RestException(HttpStatusCode.BadRequest, new { RoleCategoryListNotFound = Messages.AlreadyExist });

               }

               var roleCategoryCreated=mapper.Map<RoleCategory>(roleCategoryForCreationAndUpdateDto);
               await roleCategoryDal.Add(roleCategoryCreated);
               return mapper.Map<RoleCategory,RoleCategoryForListDto>(roleCategoryCreated);
        }

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

        public async Task<RoleCategoryForListDto> Update(RoleCategoryForCreationAndUpdateDto roleCategoryForCreationAndUpdateDto)
        {
               var roleCategory=await roleCategoryDal.GetAsync(x=>x.Id==roleCategoryForCreationAndUpdateDto.Id);
               if(roleCategory==null)
               {
                    throw new RestException(HttpStatusCode.BadRequest, new { RoleCategoryListNotFound = Messages.NotFound });
 
               }

               var updatedRoleCategory=mapper.Map(roleCategoryForCreationAndUpdateDto,roleCategory);
               await roleCategoryDal.Update(updatedRoleCategory);
               return mapper.Map<RoleCategory,RoleCategoryForListDto>(updatedRoleCategory);
        }
    }
}
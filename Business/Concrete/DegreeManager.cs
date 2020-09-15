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
using DataAccess.EntitySpecification.DegreeSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class DegreeManager : IDegreeService
    {
         private readonly IDegreeDal titleDal;
        private readonly IMapper mapper;
        public DegreeManager(IDegreeDal titleDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.titleDal = titleDal;

        }

        [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        [ValidationAspect(typeof(DegreeValidator), Priority = 2)]
        public async Task<DegreeForReturnDto> Create(DegreeForCreationDto createDto)
        {
            var checkByName = await titleDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<Degree>(createDto);
            var saveToDb = await titleDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<Degree, DegreeForReturnDto>(saveToDb);
            return mapForReturn;
        }

      [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        public async  Task<DegreeForReturnDto> Delete(int Id)
        {
             var checkFromDb = await titleDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await titleDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<Degree, DegreeForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        public async Task<Pagination<DegreeForReturnDto>> GetListAsync(DegreeParams queryParams)
        {
            var spec=new DegreeForPagingSpecification(queryParams);
            var degrees=await titleDal.ListEntityWithSpecAsync(spec);
            var countSpec=new DegreeWithFilterForCountAsync(queryParams);
            var totalCount=await titleDal.CountAsync(countSpec);
            
            if (degrees == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var data = mapper.Map<List<Degree>, List<DegreeForReturnDto>>(degrees);
            return new Pagination<DegreeForReturnDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalCount,
                data
            );
        }

        [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        public async Task<List<DegreeForReturnDto>> GetListWithoutPaging(int categoryId)
        {
             var departments = await titleDal.GetListAsync();
            if (departments == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { DepartmentListNotFound = Messages.DepartmentListNotFound });
            }

            var departmentForReturn = mapper.Map<List<Degree>, List<DegreeForReturnDto>>(departments);

            return departmentForReturn;
        }

        [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        [ValidationAspect(typeof(DegreeValidator), Priority = 2)]
        public async Task<DegreeForReturnDto> Update(DegreeForCreationDto updateDto)
        {
            var checkById = await titleDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await titleDal.Update(mapForUpdate);
            return mapper.Map<Degree, DegreeForReturnDto>(mapForUpdate);
        }
    }
}
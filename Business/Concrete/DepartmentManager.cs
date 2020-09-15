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
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDal departmentDal;
        private readonly IMapper mapper;
        public DepartmentManager(IDepartmentDal departmentDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.departmentDal = departmentDal;

        }

       [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        [ValidationAspect(typeof(DepartmentValidator), Priority = 2)]
        public async Task<DepartmentForReturnDto> Create(DepartmentForCreationDto createDto)
        {
            var checkByName = await departmentDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<Department>(createDto);
            var saveToDb = await departmentDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<Department, DepartmentForReturnDto>(saveToDb);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        public async Task<DepartmentForReturnDto> Delete(int Id)
        {
            var checkFromDb = await departmentDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await departmentDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<Department, DepartmentForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        public async Task<List<DepartmentForReturnDto>> GetDepartmentListAsync()
        {
            var departments = await departmentDal.GetListAsync();
            if (departments == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { DepartmentListNotFound = Messages.DepartmentListNotFound });
            }

            var departmentForReturn = mapper.Map<List<Department>, List<DepartmentForReturnDto>>(departments);

            return departmentForReturn;

        }

        [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        [ValidationAspect(typeof(DepartmentValidator), Priority = 2)]
        public async Task<DepartmentForReturnDto> Update(DepartmentForCreationDto updateDto)
        {
            var checkById = await departmentDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await departmentDal.Update(mapForUpdate);
            return mapper.Map<Department, DepartmentForReturnDto>(mapForUpdate);
        }
    }
}
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
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class VehicleCategoryManager : IVehicleCategoryService
    {
        private readonly IVehicleCategoryDal vehicleCategoryDal;
        private readonly IMapper mapper;
        public VehicleCategoryManager(IVehicleCategoryDal vehicleCategoryDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.vehicleCategoryDal = vehicleCategoryDal;

        }

       [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleCategoryValidator), Priority = 2)]

        public async Task<VehicleCategoryForReturnDto> Create(VehicleCategoryForCreationDto createDto)
        {
            var checkByName = await vehicleCategoryDal.GetAsync(x => x.CategoryName.ToLower() == createDto.CategoryName.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<VehicleCategory>(createDto);
            var saveToDb = await vehicleCategoryDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<VehicleCategory, VehicleCategoryForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        public async Task<VehicleCategoryForReturnDto> Delete(int Id)
        {
            var checkFromDb = await vehicleCategoryDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await vehicleCategoryDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<VehicleCategory, VehicleCategoryForReturnDto>(checkFromDb);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        public async Task<List<VehicleCategoryForReturnDto>> GetListAsync()
        {
            var vehicleCategories = await vehicleCategoryDal.GetListAsync();
            if (vehicleCategories == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<VehicleCategory>, List<VehicleCategoryForReturnDto>>(vehicleCategories);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleCategoryValidator), Priority = 2)]

        public async Task<VehicleCategoryForReturnDto> Update(VehicleCategoryForCreationDto updateDto)
        {
            var checkById = await vehicleCategoryDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await vehicleCategoryDal.Update(mapForUpdate);
            return mapper.Map<VehicleCategory, VehicleCategoryForReturnDto>(mapForUpdate);
        }
    }
}
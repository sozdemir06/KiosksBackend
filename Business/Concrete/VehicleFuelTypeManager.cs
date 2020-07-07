using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class VehicleFuelTypeManager : IVehicleFuelTypeService
    {
        private readonly IVehicleFuelTypeDal vehicleFuelTypeDal;
        private readonly IMapper mapper;
        public VehicleFuelTypeManager(IVehicleFuelTypeDal vehicleFuelTypeDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.vehicleFuelTypeDal = vehicleFuelTypeDal;

        }

        [SecuredOperation("Sudo,VehicleFuelTypes.Create", Priority = 1)]
        [ValidationAspect(typeof(VehicleFuelTypeValidator), Priority = 2)]
        public async Task<VehicleFuelTypeForReturnDto> Create(VehicleFuelTypeForCreationDto createDto)
        {
            var checkByName = await vehicleFuelTypeDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<VehicleFuelType>(createDto);
            var saveToDb = await vehicleFuelTypeDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<VehicleFuelType, VehicleFuelTypeForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,VehicleFuelTypes.Delete", Priority = 1)]
        public async Task<VehicleFuelTypeForReturnDto> Delete(int Id)
        {
            var checkFromDb = await vehicleFuelTypeDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await vehicleFuelTypeDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<VehicleFuelType, VehicleFuelTypeForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,VehicleFuelTypes.List", Priority = 1)]
        public async Task<List<VehicleFuelTypeForReturnDto>> GetListAsync()
        {
           var buildingsAgeList = await vehicleFuelTypeDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<VehicleFuelType>, List<VehicleFuelTypeForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,VehicleFuelTypes.Update", Priority = 1)]
        [ValidationAspect(typeof(VehicleFuelTypeValidator), Priority = 2)]
        public async Task<VehicleFuelTypeForReturnDto> Update(VehicleFuelTypeForCreationDto updateDto)
        {
             var checkById = await vehicleFuelTypeDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await vehicleFuelTypeDal.Update(mapForUpdate);
            return mapper.Map<VehicleFuelType, VehicleFuelTypeForReturnDto>(mapForUpdate);
        }
    }
}
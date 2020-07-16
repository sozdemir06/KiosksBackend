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
    public class VehicleGearTypeManager : IVehicleGearTypeService
    {
        private readonly IVehicleGearTypeDal vehicleGearTypeDal;
        private readonly IMapper mapper;
        public VehicleGearTypeManager(IVehicleGearTypeDal vehicleGearTypeDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.vehicleGearTypeDal = vehicleGearTypeDal;

        }
        [SecuredOperation("Sudo,VehicleGearTypes.Create", Priority = 1)]
        [ValidationAspect(typeof(VehicleGearTypeValidator), Priority = 2)]
        public async Task<VehicleGearTypeForReturnDto> Create(VehicleGearTypeForCreationDto createDto)
        {
            var checkByName = await vehicleGearTypeDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<VehicleGearType>(createDto);
            var saveToDb = await vehicleGearTypeDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<VehicleGearType, VehicleGearTypeForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,VehicleGearTypes.Delete", Priority = 1)]
        public async Task<VehicleGearTypeForReturnDto> Delete(int Id)
        {
            var checkFromDb = await vehicleGearTypeDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await vehicleGearTypeDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<VehicleGearType, VehicleGearTypeForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,VehicleGearTypes.List", Priority = 1)]
        public async Task<List<VehicleGearTypeForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await vehicleGearTypeDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<VehicleGearType>, List<VehicleGearTypeForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,VehicleGearTypes.Update", Priority = 1)]
        [ValidationAspect(typeof(VehicleGearTypeValidator), Priority = 2)]
        public async Task<VehicleGearTypeForReturnDto> Update(VehicleGearTypeForCreationDto updateDto)
        {
            var checkById = await vehicleGearTypeDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await vehicleGearTypeDal.Update(mapForUpdate);
            return mapper.Map<VehicleGearType, VehicleGearTypeForReturnDto>(mapForUpdate);
        }
    }
}
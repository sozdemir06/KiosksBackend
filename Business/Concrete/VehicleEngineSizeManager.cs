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
    public class VehicleEngineSizeManager : IVehicleEngineSizeService
    {
        private readonly IVehicleEngineSizeDal vehicleEngineSizeDal;
        private readonly IMapper mapper;
        public VehicleEngineSizeManager(IVehicleEngineSizeDal vehicleEngineSizeDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.vehicleEngineSizeDal = vehicleEngineSizeDal;

        }
       [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleEngineSizeValidator), Priority = 2)]
        public async Task<VehicleEngineSizeForReturnDto> Create(VehicleEngineSizeForCreationDto createDto)
        {
            var checkByName = await vehicleEngineSizeDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<VehicleEngineSize>(createDto);
            var saveToDb = await vehicleEngineSizeDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<VehicleEngineSize, VehicleEngineSizeForReturnDto>(saveToDb);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        public async Task<VehicleEngineSizeForReturnDto> Delete(int Id)
        {
            var checkFromDb = await vehicleEngineSizeDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await vehicleEngineSizeDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<VehicleEngineSize, VehicleEngineSizeForReturnDto>(checkFromDb);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,VehicleAnnounceOptions.All,Public", Priority = 1)]
        public async Task<List<VehicleEngineSizeForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await vehicleEngineSizeDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<VehicleEngineSize>, List<VehicleEngineSizeForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleEngineSizeValidator), Priority = 2)]
        public async Task<VehicleEngineSizeForReturnDto> Update(VehicleEngineSizeForCreationDto updateDto)
        {
            var checkById = await vehicleEngineSizeDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await vehicleEngineSizeDal.Update(mapForUpdate);
            return mapper.Map<VehicleEngineSize, VehicleEngineSizeForReturnDto>(mapForUpdate);
        }
    }
}
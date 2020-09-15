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
using DataAccess.EntitySpecification.VehicleModelSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class VehicleModelManager : IVehicleModelService
    {
        private readonly IVehicleModelDal vehicleModelDal;
        private readonly IMapper mapper;
        private readonly IVehicleBrandDal vehicleBrandDal;
        public VehicleModelManager(IVehicleModelDal vehicleModelDal, IVehicleBrandDal vehicleBrandDal, IMapper mapper)
        {
            this.vehicleBrandDal = vehicleBrandDal;
            this.mapper = mapper;
            this.vehicleModelDal = vehicleModelDal;

        }

        [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleModelValidator), Priority = 2)]
        public async Task<VehicleModelForReturnDto> Create(VehicleModelForCreationDto createDto)
        {
            var checkByName = await vehicleModelDal.GetAsync(x => x.VehicleModelName.ToLower() == createDto.VehicleModelName.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var checkBrandById = await vehicleBrandDal.GetAsync(x => x.Id == createDto.VehicleBrandId);
            if (checkBrandById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.BrandNotFound });
            }

            var mapForCreate = mapper.Map<VehicleModel>(createDto);
            var saveToDb = await vehicleModelDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<VehicleModel, VehicleModelForReturnDto>(saveToDb);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        public async Task<VehicleModelForReturnDto> Delete(int Id)
        {
            var checkFromDb = await vehicleModelDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await vehicleModelDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<VehicleModel, VehicleModelForReturnDto>(checkFromDb);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        public async Task<Pagination<VehicleModelForReturnDto>> GetListAsync(VehicleModelParams vehicleModelParams)
        {
            var spec = new VehicleModelWithBrandAndCategory(vehicleModelParams);
            var vehicleModelList = await vehicleModelDal.ListEntityWithSpecAsync(spec);
            var countSpec = new VehicleModelWithFilterCountAsync(vehicleModelParams);
            var totalCount = await vehicleModelDal.CountAsync(countSpec);

            if (vehicleModelList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var data = mapper.Map<List<VehicleModel>, List<VehicleModelForReturnDto>>(vehicleModelList);

            return new Pagination<VehicleModelForReturnDto>
            (
                vehicleModelParams.PageIndex,
                vehicleModelParams.PageSize,
                totalCount,
                data

            );
        }

       [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        public async Task<List<VehicleModelForReturnDto>> GetListByBrandIdAsync(int brandId)
        {
            var getListByVehicleModel = await vehicleModelDal.GetListAsync(x => x.VehicleBrandId == brandId);
            if (getListByVehicleModel == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }
            return mapper.Map<List<VehicleModel>, List<VehicleModelForReturnDto>>(getListByVehicleModel);
        }

        [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleModelValidator), Priority = 2)]
        public async Task<VehicleModelForReturnDto> Update(VehicleModelForCreationDto updateDto)
        {
            var checkById = await vehicleModelDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await vehicleModelDal.Update(mapForUpdate);
            return mapper.Map<VehicleModel, VehicleModelForReturnDto>(mapForUpdate);
        }
    }
}
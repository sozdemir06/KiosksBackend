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
using DataAccess.EntitySpecification.VehicleBrandSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class VehicleBrandManager : IVehicleBrandService
    {
        private readonly IVehicleBrandDal vehicleBrandDal;
        private readonly IMapper mapper;
        public VehicleBrandManager(IVehicleBrandDal vehicleBrandDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.vehicleBrandDal = vehicleBrandDal;

        }
        [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleBrandValidator), Priority = 2)]

        public async Task<VehicleBrandForReturnDto> Create(VehicleBrandForCreationDto createDto)
        {
            var checkByName = await vehicleBrandDal.GetAsync(x => x.BrandName.ToLower() == createDto.BrandName.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<VehicleBrand>(createDto);
            var saveToDb = await vehicleBrandDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<VehicleBrand, VehicleBrandForReturnDto>(saveToDb);
            return mapForReturn;
        }
        [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        public async Task<VehicleBrandForReturnDto> Delete(int Id)
        {
            var checkFromDb = await vehicleBrandDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await vehicleBrandDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<VehicleBrand, VehicleBrandForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        //[SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        public async Task<Pagination<VehicleBrandForReturnDto>> GetListAsync(VehicleBrandParams vehicleBrandParams)
        {
            var spec = new VehicleBrandWithVehicleCategorySpecification(vehicleBrandParams);
            var vehicleBrandList = await vehicleBrandDal.ListEntityWithSpecAsync(spec);
            var countSpec = new VehicleBrandWithFilterForCountAsyncSpecification(vehicleBrandParams);
            var totalCount = await vehicleBrandDal.CountAsync(countSpec);

            if (vehicleBrandList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var data = mapper.Map<List<VehicleBrand>, List<VehicleBrandForReturnDto>>(vehicleBrandList);
            return new Pagination<VehicleBrandForReturnDto>
            (
                vehicleBrandParams.PageIndex,
                vehicleBrandParams.PageSize,
                totalCount,
                data
            );
        }

        //[SecuredOperation("Sudo,VehicleAnnounceOptions.All,Public", Priority = 1)]
        public async Task<List<VehicleBrandForReturnDto>> GetListByCategoryId(int categoryId)
        {
            var getListByCategoryId = await vehicleBrandDal.GetListAsync(x => x.VehicleCategoryId == categoryId);
            if (getListByCategoryId == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<VehicleBrand>, List<VehicleBrandForReturnDto>>(getListByCategoryId);
        }


        [SecuredOperation("Sudo,VehicleAnnounceOptions.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleBrandValidator), Priority = 2)]
        public async Task<VehicleBrandForReturnDto> Update(VehicleBrandForCreationDto updateDto)
        {
            var checkById = await vehicleBrandDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await vehicleBrandDal.Update(mapForUpdate);
            return mapper.Map<VehicleBrand, VehicleBrandForReturnDto>(mapForUpdate);
        }
    }
}
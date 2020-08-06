using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Helpers;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.QueryParams;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.VehicleAnnounceSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class VehicleAnnounceManager : IVehicleAnnounceService
    {
        private readonly IVehicleAnnounceDal vehicleAnnounceDal;
        private readonly IMapper mapper;
        private readonly IVehicleAnnounceSubScreenDal vehicleAnnounceSubScreenDal;
        public VehicleAnnounceManager(IVehicleAnnounceDal vehicleAnnounceDal, IMapper mapper, IVehicleAnnounceSubScreenDal vehicleAnnounceSubScreenDal)
        {
            this.vehicleAnnounceSubScreenDal = vehicleAnnounceSubScreenDal;
            this.mapper = mapper;
            this.vehicleAnnounceDal = vehicleAnnounceDal;

        }
        public async Task<VehicleAnnounceForReturnDto> Create(VehicleAnnounceForCreationDto creationDto)
        {
            var checkByNameFromRepo = await vehicleAnnounceDal.GetAsync(x => x.Header.ToLower() == creationDto.Header.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<VehicleAnnounce>(creationDto);
            var slideId=System.Guid.NewGuid();
            mapForCreate.SlideId = slideId;
            mapForCreate.Created = DateTime.Now;
            mapForCreate.AnnounceType="Car";

            var createHomeAnnounce = await vehicleAnnounceDal.Add(mapForCreate);
            return mapper.Map<VehicleAnnounce, VehicleAnnounceForReturnDto>(createHomeAnnounce);
        }

        public async Task<VehicleAnnounceForReturnDto> Delete(int Id)
        {
            var getByIdFromRepo = await vehicleAnnounceDal.GetAsync(x => x.Id == Id);
            if (getByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await vehicleAnnounceDal.Delete(getByIdFromRepo);
            return mapper.Map<VehicleAnnounce, VehicleAnnounceForReturnDto>(getByIdFromRepo);
        }

        public async Task<VehicleAnnounceForDetailDto> GetDetailAsync(int vehicleAnnounceId)
        {

            var spec = new VehicleAnnounceDetailSpecification(vehicleAnnounceId);
            var getDetailFromRepo = await vehicleAnnounceDal.GetEntityWithSpecAsync(spec);

            if (getDetailFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<VehicleAnnounce, VehicleAnnounceForDetailDto>(getDetailFromRepo);

        }

        public async Task<Pagination<VehicleAnnounceForReturnDto>> GetListAsync(VehicleAnnounceParams queryParams)
        {
            var spec = new VehicleAnnounceWithPagingSpecification(queryParams);
            var listFromRepo = await vehicleAnnounceDal.ListEntityWithSpecAsync(spec);
            var countSpec = new VehicleAnnounceWithFilterForCountSpecification(queryParams);
            var totalItem = await vehicleAnnounceDal.CountAsync(countSpec);

            if (listFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.HomeAnnounceEmpty });
            }

            var data = mapper.Map<List<VehicleAnnounce>, List<VehicleAnnounceForReturnDto>>(listFromRepo);
            return new Pagination<VehicleAnnounceForReturnDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItem,
                data
            );
        }

        public async Task<VehicleAnnounceForReturnDto> Publish(VehicleAnnounceForCreationDto updateDto)
        {
            var checkFromRepo = await vehicleAnnounceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var checkAnnounceSubScreenForPublish = await vehicleAnnounceSubScreenDal.GetListAsync(x => x.VehicleAnnounceId == updateDto.Id);
            if (checkAnnounceSubScreenForPublish == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotSelectSubScreen = Messages.NotSelectSubScreen });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            var updateToDb = await vehicleAnnounceDal.Update(mapForUpdate);
            
            return mapper.Map<VehicleAnnounce, VehicleAnnounceForReturnDto>(updateToDb);
        }

        public async Task<VehicleAnnounceForReturnDto> Update(VehicleAnnounceForCreationDto updateDto)
        {
            var checkFromRepo = await vehicleAnnounceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            await vehicleAnnounceDal.Update(mapForUpdate);
            var spec=new VehicleAnnounceWithPagingSpecification(checkFromRepo.Id);
            var getWithUser=await vehicleAnnounceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<VehicleAnnounce, VehicleAnnounceForReturnDto>(getWithUser);
        }
    }
}
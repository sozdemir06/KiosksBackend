using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
using DataAccess.EntitySpecification.VehicleAnnounceSpecification;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class VehicleAnnounceManager : IVehicleAnnounceService
    {
        private readonly IVehicleAnnounceDal vehicleAnnounceDal;
        private readonly IMapper mapper;
        private readonly IVehicleAnnounceSubScreenDal vehicleAnnounceSubScreenDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        public VehicleAnnounceManager(IVehicleAnnounceDal vehicleAnnounceDal, IHttpContextAccessor httpContextAccessor,
        IMapper mapper, IVehicleAnnounceSubScreenDal vehicleAnnounceSubScreenDal)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.vehicleAnnounceSubScreenDal = vehicleAnnounceSubScreenDal;
            this.mapper = mapper;
            this.vehicleAnnounceDal = vehicleAnnounceDal;

        }

        [SecuredOperation("Sudo,VehicleAnnounces.Create,VehicleAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleAnnounceValidator), Priority = 2)]
        public async Task<VehicleAnnounceForReturnDto> Create(VehicleAnnounceForCreationDto creationDto)
        {
            var checkByNameFromRepo = await vehicleAnnounceDal.GetAsync(x => x.Header.ToLower() == creationDto.Header.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<VehicleAnnounce>(creationDto);
            var slideId = System.Guid.NewGuid();
            mapForCreate.SlideId = slideId;
            mapForCreate.Created = DateTime.Now;
            mapForCreate.AnnounceType = "Car";

            var createHomeAnnounce = await vehicleAnnounceDal.Add(mapForCreate);
            return mapper.Map<VehicleAnnounce, VehicleAnnounceForReturnDto>(createHomeAnnounce);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(VehicleAnnounceValidator), Priority = 2)]
        public async Task<VehicleAnnounceForUserDto> CreateForPublicAsync(VehicleAnnounceForCreationDto creationDto, int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }
            var checkByNameFromRepo = await vehicleAnnounceDal.GetAsync(x => x.Header.ToLower() == creationDto.Header.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<VehicleAnnounce>(creationDto);
            var slideId = System.Guid.NewGuid();
            mapForCreate.UserId = claimId;
            mapForCreate.IsNew = true;
            mapForCreate.IsPublish = false;
            mapForCreate.Reject = false;
            mapForCreate.SlideIntervalTime = 8;
            mapForCreate.PublishFinishDate = DateTime.Now;
            mapForCreate.PublishStartDate = DateTime.Now;
            mapForCreate.SlideId = slideId;
            mapForCreate.Created = DateTime.Now;
            mapForCreate.AnnounceType = "Car";

            var createHomeAnnounce = await vehicleAnnounceDal.Add(mapForCreate);
            var spec = new VehicleAnnounceByUserIdSpecification(userId, createHomeAnnounce.Id);

            var getAnnounceFromRepo = await vehicleAnnounceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<VehicleAnnounce, VehicleAnnounceForUserDto>(getAnnounceFromRepo);
        }

        [SecuredOperation("Sudo,VehicleAnnounces.Delete,VehicleAnnounces.All", Priority = 1)]
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

        [SecuredOperation("Sudo,VehicleAnnounces.List,VehicleAnnounces.All", Priority = 1)]
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

        [SecuredOperation("Sudo,VehicleAnnounces.List,VehicleAnnounces.All", Priority = 1)]
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

        [SecuredOperation("Sudo,VehicleAnnounces.Publish,VehicleAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleAnnounceValidator), Priority = 2)]
        public async Task<VehicleAnnounceForReturnDto> Publish(VehicleAnnounceForCreationDto updateDto)
        {
            var checkFromRepo = await vehicleAnnounceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var checkAnnounceSubScreenForPublish = await vehicleAnnounceSubScreenDal.GetListAsync(x => x.VehicleAnnounceId == updateDto.Id);
            if (checkAnnounceSubScreenForPublish.Count <= 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotSelectSubScreen = Messages.NotSelectSubScreen });
            }


            if (updateDto.IsPublish)
            {
                var checkDateExpire = DateTime.Compare(DateTime.Now, checkFromRepo.PublishFinishDate);
                if (checkDateExpire > 0)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.PublishDateExpire });
                }

            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            var updateToDb = await vehicleAnnounceDal.Update(mapForUpdate);

            return mapper.Map<VehicleAnnounce, VehicleAnnounceForReturnDto>(updateToDb);
        }

        [SecuredOperation("Sudo,VehicleAnnounces.Update,VehicleAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleAnnounceValidator), Priority = 2)]
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
            var spec = new VehicleAnnounceWithPagingSpecification(checkFromRepo.Id);
            var getWithUser = await vehicleAnnounceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<VehicleAnnounce, VehicleAnnounceForReturnDto>(getWithUser);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(VehicleAnnounceValidator), Priority = 2)]
        public async Task<VehicleAnnounceForUserDto> UpdateForPublicAsync(VehicleAnnounceForCreationDto updateDto, int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }
            var checkFromRepo = await vehicleAnnounceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            mapForUpdate.IsNew = true;
            mapForUpdate.IsPublish = false;
            mapForUpdate.Reject = false;
            await vehicleAnnounceDal.Update(mapForUpdate);
            var spec = new VehicleAnnounceByUserIdSpecification(userId, checkFromRepo.Id);
            var getWithUser = await vehicleAnnounceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<VehicleAnnounce, VehicleAnnounceForUserDto>(getWithUser);
        }
    }
}
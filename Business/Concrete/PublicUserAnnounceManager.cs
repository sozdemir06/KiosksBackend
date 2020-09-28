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
using Core.Utilities.Photos;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.AnnounceSpecification;
using DataAccess.EntitySpecification.HomeAnnounceSpecification;
using DataAccess.EntitySpecification.VehicleAnnounceSpecification;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class PublicUserAnnounceManager : IPublicUserAnnounceService
    {
        private readonly IMapper mapper;
        private readonly IAnnounceDal announceDal;
        private readonly IHomeAnnounceDal homeAnnounceDal;
        private readonly IAnnouncePhotoDal announcePhotoDal;
        private readonly IUploadFile upload;
        private readonly IVehicleAnnounceDal vehicleAnnounceDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserDal userDal;

        public PublicUserAnnounceManager(IMapper mapper,
        IAnnounceDal announceDal, IHomeAnnounceDal homeAnnounceDal, IAnnouncePhotoDal announcePhotoDal,
        IUploadFile upload,
         IVehicleAnnounceDal vehicleAnnounceDal,
        IHttpContextAccessor httpContextAccessor,
        IUserDal userDal)
        {
            this.userDal = userDal;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.announceDal = announceDal;
            this.homeAnnounceDal = homeAnnounceDal;
            this.announcePhotoDal = announcePhotoDal;
            this.upload = upload;
            this.vehicleAnnounceDal = vehicleAnnounceDal;
        }


        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<Pagination<AnnounceForUserDto>> GetAnnounceByUserIdAsync(AnnounceParams queryParams, int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            var userFromRepo = await userDal.GetAsync(x => x.Id == userId);
            if (userFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.UserNotFound });
            }

            var spec = new AnnounceByUserIdSpecification(queryParams, userId);
            var announces = await announceDal.ListEntityWithSpecAsync(spec);
            var countSpec = new AnnounceByUserIdSpecification(userId);
            var totalItem = await announceDal.CountAsync(countSpec);

            var data = mapper.Map<List<Announce>, List<AnnounceForUserDto>>(announces);
            return new Pagination<AnnounceForUserDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItem,
                data
            );


        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<Pagination<HomeAnnounceForUserDto>> GetHomeAnnounceByUserIdAsync(HomeAnnounceParams queryParams, int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            var userFromRepo = await userDal.GetAsync(x => x.Id == userId);
            if (userFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.UserNotFound });
            }

            var spec = new HomeAnnounceByUserIdSpecification(queryParams, userId);
            var homeAnnounces = await homeAnnounceDal.ListEntityWithSpecAsync(spec);
            var countSpec = new HomeAnnounceByUserIdSpecification(userId);
            var totalItem = await homeAnnounceDal.CountAsync(countSpec);

            var data = mapper.Map<List<HomeAnnounce>, List<HomeAnnounceForUserDto>>(homeAnnounces);
            return new Pagination<HomeAnnounceForUserDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItem,
                data
            );
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<Pagination<VehicleAnnounceForUserDto>> GetVehicleAnnounceByUserIdAsync(VehicleAnnounceParams queryParams, int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            var userFromRepo = await userDal.GetAsync(x => x.Id == userId);
            if (userFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.UserNotFound });
            }

            var spec = new VehicleAnnounceByUserIdSpecification(queryParams, userId);
            var vehicleAnnounces = await vehicleAnnounceDal.ListEntityWithSpecAsync(spec);
            var countSpec = new VehicleAnnounceByUserIdSpecification(userId);
            var totalItem = await vehicleAnnounceDal.CountAsync(countSpec);

            var data = mapper.Map<List<VehicleAnnounce>, List<VehicleAnnounceForUserDto>>(vehicleAnnounces);
            return new Pagination<VehicleAnnounceForUserDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItem,
                data
            );
        }


    }
}
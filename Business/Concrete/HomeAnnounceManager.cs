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
using Core.Aspects.AutoFac.Logging;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.QueryParams;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.HomeAnnounceSpecification;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class HomeAnnounceManager : IHomeAnnounceService
    {
        private readonly IHomeAnnounceDal homeAnnounceDal;
        private readonly IMapper mapper;
        private readonly IHomeAnnounceSubScreenDal homeAnnounceSubScreenDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        public HomeAnnounceManager(IHomeAnnounceDal homeAnnounceDal, IHttpContextAccessor httpContextAccessor,
        IMapper mapper, IHomeAnnounceSubScreenDal homeAnnounceSubScreenDal)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.homeAnnounceSubScreenDal = homeAnnounceSubScreenDal;
            this.mapper = mapper;
            this.homeAnnounceDal = homeAnnounceDal;

        }

        [SecuredOperation("Sudo,HomeAnnounces.Create,HomeAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<HomeAnnounceForReturnDto> Create(HomeAnnounceForCreationDto creationDto)
        {
            var checkByNameFromRepo = await homeAnnounceDal.GetAsync(x => x.Header.ToLower() == creationDto.Header.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<HomeAnnounce>(creationDto);
            var slideId = Guid.NewGuid();
            mapForCreate.AnnounceType = "home";
            mapForCreate.SlideId = slideId;
            mapForCreate.Created = DateTime.Now;

            var createHomeAnnounce = await homeAnnounceDal.Add(mapForCreate);
            var spec = new HomeAnnounceWithPhotoAndUserSpecification(createHomeAnnounce.Id);

            var getAnnounceFromRepo = await homeAnnounceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<HomeAnnounce, HomeAnnounceForReturnDto>(getAnnounceFromRepo);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<HomeAnnounceForUserDto> CreateForPublicAsync(HomeAnnounceForCreationDto creationDto, int userId)
        {
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            var checkByNameFromRepo = await homeAnnounceDal.GetAsync(x => x.Header.ToLower() == creationDto.Header.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<HomeAnnounce>(creationDto);
            var slideId = Guid.NewGuid();
            mapForCreate.UserId = claimId;
            mapForCreate.IsNew = true;
            mapForCreate.IsPublish = false;
            mapForCreate.Reject = false;
            mapForCreate.SlideIntervalTime = 8;
            mapForCreate.PublishFinishDate = DateTime.Now;
            mapForCreate.PublishStartDate = DateTime.Now;
            mapForCreate.AnnounceType = "home";
            mapForCreate.SlideId = slideId;
            mapForCreate.Created = DateTime.Now;

            var createHomeAnnounce = await homeAnnounceDal.Add(mapForCreate);
            var spec = new HomeAnnounceByUserIdSpecification(userId, createHomeAnnounce.Id);

            var getAnnounceFromRepo = await homeAnnounceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<HomeAnnounce, HomeAnnounceForUserDto>(getAnnounceFromRepo);
        }

        [SecuredOperation("Sudo,HomeAnnounces.Delete,HomeAnnounces.All", Priority = 1)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<HomeAnnounceForReturnDto> Delete(int Id)
        {
            var getByIdFromRepo = await homeAnnounceDal.GetAsync(x => x.Id == Id);
            if (getByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await homeAnnounceDal.Delete(getByIdFromRepo);
            return mapper.Map<HomeAnnounce, HomeAnnounceForReturnDto>(getByIdFromRepo);
        }



        //[SecuredOperation("Sudo,HomeAnnounces.List,HomeAnnounces.All", Priority = 1)]
        public async Task<Pagination<HomeAnnounceForReturnDto>> GetListAsync(HomeAnnounceParams queryParams)
        {
            var spec = new HomeAnnounceWithPhotoAndUserSpecification(queryParams);
            var listFromRepo = await homeAnnounceDal.ListEntityWithSpecAsync(spec);
            var countSpec = new HomeAnnounceWithFilterForCountSpecification(queryParams);
            var totalItem = await homeAnnounceDal.CountAsync(countSpec);



            var data = mapper.Map<List<HomeAnnounce>, List<HomeAnnounceForReturnDto>>(listFromRepo);
            return new Pagination<HomeAnnounceForReturnDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItem,
                data
            );

        }

        [SecuredOperation("Sudo,HomeAnnounces.Publish,HomeAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<HomeAnnounceForReturnDto> Publish(HomeAnnounceForCreationDto updateDto)
        {
            var checkFromRepo = await homeAnnounceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }


            var checkHomeAnnounceSubScreenForPublish = await homeAnnounceSubScreenDal.GetListAsync(x => x.HomeAnnounceId == updateDto.Id);
            if (checkHomeAnnounceSubScreenForPublish.Count <= 0)
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
            mapForUpdate.AnnounceType = "home";
            await homeAnnounceDal.Update(mapForUpdate);

            var spec = new HomeAnnounceWithPhotoAndUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await homeAnnounceDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<HomeAnnounce, HomeAnnounceForReturnDto>(getAnnounceWithUserFromRepo);

        }

        [SecuredOperation("Sudo,HomeAnnounces.Update,HomeAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<HomeAnnounceForReturnDto> Update(HomeAnnounceForCreationDto updateDto)
        {
            var checkFromRepo = await homeAnnounceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            mapForUpdate.AnnounceType = "home";
            await homeAnnounceDal.Update(mapForUpdate);

            var spec = new HomeAnnounceWithPhotoAndUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await homeAnnounceDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<HomeAnnounce, HomeAnnounceForReturnDto>(getAnnounceWithUserFromRepo);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<HomeAnnounceForUserDto> UpdateForPublicAsync(HomeAnnounceForCreationDto creationDto, int userId)
        {


            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != userId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            var checkFromRepo = await homeAnnounceDal.GetAsync(x => x.Id == creationDto.Id);

            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(creationDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            mapForUpdate.IsNew = true;
            mapForUpdate.AnnounceType = "home";
            mapForUpdate.IsPublish = false;
            mapForUpdate.Reject = false;
            await homeAnnounceDal.Update(mapForUpdate);

            var spec = new HomeAnnounceByUserIdSpecification(userId, creationDto.Id);
            var getAnnounceWithUserFromRepo = await homeAnnounceDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<HomeAnnounce, HomeAnnounceForUserDto>(getAnnounceWithUserFromRepo);
        }
    }
}
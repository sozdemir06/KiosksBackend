using System;
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
using DataAccess.EntitySpecification.LiveTvBroadCasts;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class LiveTvBroadCastManager : ILiveTvBroadCastService
    {
        private readonly ILiveTvBroadCastDal liveTvBrodcastDal;
        private readonly IMapper mapper;
        private readonly ILiveTvBroadCastSubScreenDal liveTvBroadCastSubScreenDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        public LiveTvBroadCastManager(ILiveTvBroadCastDal liveTvBrodcastDal, IMapper mapper, IHttpContextAccessor httpContextAccessor,
        ILiveTvBroadCastSubScreenDal liveTvBroadCastSubScreenDal)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.liveTvBroadCastSubScreenDal = liveTvBroadCastSubScreenDal;
            this.mapper = mapper;
            this.liveTvBrodcastDal = liveTvBrodcastDal;

        }

        [SecuredOperation("Sudo,LiveTvBroadCasts.Create,LiveTvBroadCasts.All", Priority = 1)]
        [ValidationAspect(typeof(LiveTvBroadCastValidator), Priority = 2)]
        public async Task<LiveTvBroadCastForReturnDto> Create(LiveTvBroadCastForCreationDto creationDto)
        {
            var checkByNameFromRepo = await liveTvBrodcastDal.GetAsync(x => x.Header.ToLower() == creationDto.Header.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var userId = (int)httpContextAccessor.HttpContext.User?.ClaimsId();

            var mapForCreate = mapper.Map<LiveTvBroadCast>(creationDto);
            var slideId = Guid.NewGuid();
            mapForCreate.SlideId = slideId;
            mapForCreate.UserId=userId;
            mapForCreate.SlideIntervalTime = (int)TimeSpan.FromMinutes(mapForCreate.SlideIntervalTime).TotalMilliseconds;
            mapForCreate.Created = DateTime.Now;
            mapForCreate.AnnounceType = "livetv";

            var createAnnounce = await liveTvBrodcastDal.Add(mapForCreate);
            var spec = new LiveTvBroadCastWithUserSpecification(createAnnounce.Id);

            var getAnnounceFromRepo = await liveTvBrodcastDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<LiveTvBroadCast, LiveTvBroadCastForReturnDto>(getAnnounceFromRepo);
        }

        [SecuredOperation("Sudo,LiveTvBroadCasts.Delete,LiveTvBroadCasts.All", Priority = 1)]
        public async Task<LiveTvBroadCastForReturnDto> Delete(int Id)
        {
            var getByIdFromRepo = await liveTvBrodcastDal.GetAsync(x => x.Id == Id);
            if (getByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await liveTvBrodcastDal.Delete(getByIdFromRepo);
            return mapper.Map<LiveTvBroadCast, LiveTvBroadCastForReturnDto>(getByIdFromRepo);
        }

        [SecuredOperation("Sudo,LiveTvBroadCasts.List,LiveTvBroadCasts.All", Priority = 1)]
        public async Task<LiveTvBroadCastForDetailDto> GetDetailAsync(int announceId)
        {
            var spec = new LiveTvBroadCastWithDetailSpecification(announceId);
            var getDetailFromRepo = await liveTvBrodcastDal.GetEntityWithSpecAsync(spec);

            if (getDetailFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<LiveTvBroadCast, LiveTvBroadCastForDetailDto>(getDetailFromRepo);
        }

        [SecuredOperation("Sudo,LiveTvBroadCasts.List,LiveTvBroadCasts.All", Priority = 1)]
        public async Task<Pagination<LiveTvBroadCastForReturnDto>> GetListAsync(LiveTvBroadCastParams queryParams)
        {
            var spec = new LiveTvBroadCastWithPagingSpecification(queryParams);
            var listFromRepo = await liveTvBrodcastDal.ListEntityWithSpecAsync(spec);
            var countSpec = new LiveTvBroadCAstWithFilterForCaountAsyncSpecification(queryParams);
            var totalItem = await liveTvBrodcastDal.CountAsync(countSpec);

            if (listFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.HomeAnnounceEmpty });
            }

            var data = mapper.Map<List<LiveTvBroadCast>, List<LiveTvBroadCastForReturnDto>>(listFromRepo);
            return new Pagination<LiveTvBroadCastForReturnDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItem,
                data
            );
        }

        [SecuredOperation("Sudo,LiveTvBroadCasts.Publish,LiveTvBroadCasts.All", Priority = 1)]
        [ValidationAspect(typeof(LiveTvBroadCastValidator), Priority = 2)]
        public async Task<LiveTvBroadCastForReturnDto> Publish(LiveTvBroadCastForCreationDto updateDto)
        {
            var checkFromRepo = await liveTvBrodcastDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var checkHomeAnnounceSubScreenForPublish = await liveTvBroadCastSubScreenDal.GetListAsync(x => x.LiveTvBroadCastId == updateDto.Id);
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
            mapForUpdate.SlideIntervalTime=(int)TimeSpan.FromMinutes(mapForUpdate.SlideIntervalTime).TotalMilliseconds;
            await liveTvBrodcastDal.Update(mapForUpdate);

            var spec = new LiveTvBroadCastWithUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await liveTvBrodcastDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<LiveTvBroadCast, LiveTvBroadCastForReturnDto>(getAnnounceWithUserFromRepo);
        }


        [SecuredOperation("Sudo,LiveTvBroadCasts.Update,LiveTvBroadCasts.All", Priority = 1)]
        [ValidationAspect(typeof(LiveTvBroadCastValidator), Priority = 2)]

        public async Task<LiveTvBroadCastForReturnDto> Update(LiveTvBroadCastForCreationDto updateDto)
        {
            var checkFromRepo = await liveTvBrodcastDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            mapForUpdate.SlideIntervalTime=(int)TimeSpan.FromMinutes(mapForUpdate.SlideIntervalTime).TotalMilliseconds;
            await liveTvBrodcastDal.Update(mapForUpdate);

            var spec = new LiveTvBroadCastWithUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await liveTvBrodcastDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<LiveTvBroadCast, LiveTvBroadCastForReturnDto>(getAnnounceWithUserFromRepo);
        }
    }
}
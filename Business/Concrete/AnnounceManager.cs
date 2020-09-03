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
using DataAccess.EntitySpecification.AnnounceSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AnnounceManager : IAnnounceService
    {
        private readonly IAnnounceDal announceDal;
        private readonly IMapper mapper;
        private readonly IAnnounceSubScreenDal announceSubScreenDal;
        public AnnounceManager(IAnnounceDal announceDal, IMapper mapper, IAnnounceSubScreenDal announceSubScreenDal)
        {
            this.announceSubScreenDal = announceSubScreenDal;
            this.mapper = mapper;
            this.announceDal = announceDal;

        }

        [SecuredOperation("Sudo,Announces.Create,Announces.All", Priority = 1)]
        [ValidationAspect(typeof(AnnounceValidator), Priority = 2)]
        public async Task<AnnounceForReturnDto> Create(AnnounceForCreationDto creationDto)
        {
            var checkByNameFromRepo = await announceDal.GetAsync(x => x.Header.ToLower() == creationDto.Header.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<Announce>(creationDto);
            var slideId = Guid.NewGuid();
            mapForCreate.SlideId = slideId;
            mapForCreate.Created = DateTime.Now;
            mapForCreate.AnnounceType = "announce";

            var createAnnounce = await announceDal.Add(mapForCreate);
            var spec = new AnnounceWithUserSpecification(createAnnounce.Id);

            var getAnnounceFromRepo = await announceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<Announce, AnnounceForReturnDto>(getAnnounceFromRepo);
        }

        [SecuredOperation("Sudo,Announces.Delete,Announces.All", Priority = 1)]
        public async Task<AnnounceForReturnDto> Delete(int Id)
        {
            var getByIdFromRepo = await announceDal.GetAsync(x => x.Id == Id);
            if (getByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await announceDal.Delete(getByIdFromRepo);
            return mapper.Map<Announce, AnnounceForReturnDto>(getByIdFromRepo);
        }

        [SecuredOperation("Sudo,Announces.List,Announces.All", Priority = 1)]
        public async Task<AnnounceForDetailDto> GetDetailAsync(int announceId)
        {
            var spec = new AnnounceWithDetailSpecification(announceId);
            var getDetailFromRepo = await announceDal.GetEntityWithSpecAsync(spec);

            if (getDetailFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<Announce, AnnounceForDetailDto>(getDetailFromRepo);
        }

        [SecuredOperation("Sudo,Announces.List,Announces.All", Priority = 1)]
        public async Task<Pagination<AnnounceForReturnDto>> GetListAsync(AnnounceParams queryParams)
        {
            var spec = new AnnounceWithPagingSpecification(queryParams);
            var listFromRepo = await announceDal.ListEntityWithSpecAsync(spec);
            var countSpec = new AnnounceWithFilterForCaountAsyncSpecification(queryParams);
            var totalItem = await announceDal.CountAsync(countSpec);

            if (listFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.HomeAnnounceEmpty });
            }

            var data = mapper.Map<List<Announce>, List<AnnounceForReturnDto>>(listFromRepo);
            return new Pagination<AnnounceForReturnDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItem,
                data
            );
        }

        [SecuredOperation("Sudo,Announces.Publish,Announces.All", Priority = 1)]
        [ValidationAspect(typeof(AnnounceValidator), Priority = 2)]
        public async Task<AnnounceForReturnDto> Publish(AnnounceForCreationDto updateDto)
        {
            var checkFromRepo = await announceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var checkHomeAnnounceSubScreenForPublish = await announceSubScreenDal.GetListAsync(x => x.AnnounceId == updateDto.Id);
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
            await announceDal.Update(mapForUpdate);

            var spec = new AnnounceWithUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await announceDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<Announce, AnnounceForReturnDto>(getAnnounceWithUserFromRepo);
        }

        [SecuredOperation("Sudo,Announces.Update,Announces.All", Priority = 1)]
        [ValidationAspect(typeof(AnnounceValidator), Priority = 2)]
        public async Task<AnnounceForReturnDto> Update(AnnounceForCreationDto updateDto)
        {
            var checkFromRepo = await announceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            await announceDal.Update(mapForUpdate);

            var spec = new AnnounceWithUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await announceDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<Announce, AnnounceForReturnDto>(getAnnounceWithUserFromRepo);
        }
    }
}
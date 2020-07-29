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
using DataAccess.EntitySpecification.HomeAnnounceSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class HomeAnnounceManager : IHomeAnnounceService
    {
        private readonly IHomeAnnounceDal homeAnnounceDal;
        private readonly IMapper mapper;
        private readonly IHomeAnnounceSubScreenDal homeAnnounceSubScreenDal;
        public HomeAnnounceManager(IHomeAnnounceDal homeAnnounceDal, IMapper mapper, IHomeAnnounceSubScreenDal homeAnnounceSubScreenDal)
        {
            this.homeAnnounceSubScreenDal = homeAnnounceSubScreenDal;
            this.mapper = mapper;
            this.homeAnnounceDal = homeAnnounceDal;

        }

        [SecuredOperation("Sudo,HomeAnnounces.Create,HomeAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceValidator), Priority = 2)]
        public async Task<HomeAnnounceForReturnDto> Create(HomeAnnounceForCreationDto creationDto)
        {
            var checkByNameFromRepo = await homeAnnounceDal.GetAsync(x => x.Header.ToLower() == creationDto.Header.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<HomeAnnounce>(creationDto);
            mapForCreate.SlideId = Guid.NewGuid();
            mapForCreate.Created = DateTime.Now;

            var createHomeAnnounce = await homeAnnounceDal.Add(mapForCreate);
            var spec = new HomeAnnounceWithPhotoAndUserSpecification(createHomeAnnounce.Id);

            var getAnnounceFromRepo = await homeAnnounceDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<HomeAnnounce, HomeAnnounceForReturnDto>(getAnnounceFromRepo);
        }

        [SecuredOperation("Sudo,HomeAnnounces.Delete,HomeAnnounces.All", Priority = 1)]
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

        [SecuredOperation("Sudo,HomeAnnounces.List,HomeAnnounces.All", Priority = 1)]
        public async Task<Pagination<HomeAnnounceForReturnDto>> GetListAsync(HomeAnnounceParams queryParams)
        {
            var spec = new HomeAnnounceWithPhotoAndUserSpecification(queryParams);
            var listFromRepo = await homeAnnounceDal.ListEntityWithSpecAsync(spec);
            var countSpec = new HomeAnnounceWithFilterForCountSpecification(queryParams);
            var totalItem = await homeAnnounceDal.CountAsync(countSpec);

            if (listFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.HomeAnnounceEmpty });
            }

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
        public async Task<HomeAnnounceForReturnDto> Publish(HomeAnnounceForCreationDto updateDto)
        {
            var checkFromRepo = await homeAnnounceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var checkHomeAnnounceSubScreenForPublish = await homeAnnounceSubScreenDal.GetListAsync(x=>x.HomeAnnounceId==updateDto.Id);
            if(checkHomeAnnounceSubScreenForPublish==null)
            {
               throw new RestException(HttpStatusCode.BadRequest, new { NotSelectSubScreen = Messages.NotSelectSubScreen }); 
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            await homeAnnounceDal.Update(mapForUpdate);

            var spec = new HomeAnnounceWithPhotoAndUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await homeAnnounceDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<HomeAnnounce, HomeAnnounceForReturnDto>(getAnnounceWithUserFromRepo);

        }

        [SecuredOperation("Sudo,HomeAnnounces.Update,HomeAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceValidator), Priority = 2)]
        public async Task<HomeAnnounceForReturnDto> Update(HomeAnnounceForCreationDto updateDto)
        {
            var checkFromRepo = await homeAnnounceDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            await homeAnnounceDal.Update(mapForUpdate);

            var spec = new HomeAnnounceWithPhotoAndUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await homeAnnounceDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<HomeAnnounce, HomeAnnounceForReturnDto>(getAnnounceWithUserFromRepo);
        }
    }
}
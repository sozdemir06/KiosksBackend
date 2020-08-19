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
using DataAccess.EntitySpecification.FoodMenuSpecification;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class FoodMenuManager : IFoodMenuService
    {
        private readonly IFoodMenuDal foodMenuDal;
        private readonly IMapper mapper;
        private readonly IFoodMenuSubScreenDal foodMenuSubScreenDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        public FoodMenuManager(IFoodMenuDal foodMenuDal, IMapper mapper, IFoodMenuSubScreenDal foodMenuSubScreenDal, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.foodMenuSubScreenDal = foodMenuSubScreenDal;
            this.mapper = mapper;
            this.foodMenuDal = foodMenuDal;

        }

        [SecuredOperation("Sudo,FoodMenu.Create,FoodMenu.All", Priority = 1)]
        [ValidationAspect(typeof(FoodMenuValidator), Priority = 2)]
        public async Task<FoodMenuForReturnDto> Create(FoodMenuForCreationDto creationDto)
        {
        var claimId=int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);


            var mapForCreate = mapper.Map<FoodMenu>(creationDto);
            var slideId = Guid.NewGuid();
            mapForCreate.SlideId = slideId;
            mapForCreate.UserId=claimId;
            mapForCreate.Created = DateTime.Now;
            mapForCreate.AnnounceType = "foodmenu";

            var createAnnounce = await foodMenuDal.Add(mapForCreate);
            var spec = new FoodMenuWithUserSpecification(createAnnounce.Id);

            var getAnnounceFromRepo = await foodMenuDal.GetEntityWithSpecAsync(spec);
            if (getAnnounceFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.NotFound });
            }
            return mapper.Map<FoodMenu, FoodMenuForReturnDto>(getAnnounceFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenu.Delete,FoodMenu.All", Priority = 1)]
        public async Task<FoodMenuForReturnDto> Delete(int Id)
        {
            var getByIdFromRepo = await foodMenuDal.GetAsync(x => x.Id == Id);
            if (getByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await foodMenuDal.Delete(getByIdFromRepo);
            return mapper.Map<FoodMenu, FoodMenuForReturnDto>(getByIdFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenu.List,FoodMenu.All", Priority = 1)]
        public async Task<FoodMenuForDetailDto> GetDetailAsync(int announceId)
        {
            var spec = new FoodMenuWithDetailSpecification(announceId);
            var getDetailFromRepo = await foodMenuDal.GetEntityWithSpecAsync(spec);

            if (getDetailFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<FoodMenu, FoodMenuForDetailDto>(getDetailFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenu.List,FoodMenu.All", Priority = 1)]
        public async Task<Pagination<FoodMenuForReturnDto>> GetListAsync(FoodMenuParams queryParams)
        {
            var spec = new FoodMenuWithPagingSpecification(queryParams);
            var listFromRepo = await foodMenuDal.ListEntityWithSpecAsync(spec);
            var countSpec = new FoodMenuWithFilterForCountAsyncSpecification(queryParams);
            var totalItem = await foodMenuDal.CountAsync(countSpec);

            if (listFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.HomeAnnounceEmpty });
            }

            var data = mapper.Map<List<FoodMenu>, List<FoodMenuForReturnDto>>(listFromRepo);
            return new Pagination<FoodMenuForReturnDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItem,
                data
            );
        }

        [SecuredOperation("Sudo,FoodMenu.Publish,FoodMenu.All", Priority = 1)]
        public async Task<FoodMenuForReturnDto> Publish(FoodMenuForCreationDto updateDto)
        {
            var checkFromRepo = await foodMenuDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var checkHomeAnnounceSubScreenForPublish = await foodMenuSubScreenDal.GetListAsync(x => x.FoodMenuId == updateDto.Id);
            if (checkHomeAnnounceSubScreenForPublish == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotSelectSubScreen = Messages.NotSelectSubScreen });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            await foodMenuDal.Update(mapForUpdate);

            var spec = new FoodMenuWithUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await foodMenuDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<FoodMenu, FoodMenuForReturnDto>(getAnnounceWithUserFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenu.Update,FoodMenu.All", Priority = 1)]
        [ValidationAspect(typeof(FoodMenuValidator), Priority = 2)]
        public async Task<FoodMenuForReturnDto> Update(FoodMenuForCreationDto updateDto)
        {
            var checkFromRepo = await foodMenuDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            await foodMenuDal.Update(mapForUpdate);

            var spec = new FoodMenuWithUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await foodMenuDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<FoodMenu, FoodMenuForReturnDto>(getAnnounceWithUserFromRepo);
        }
    }
}
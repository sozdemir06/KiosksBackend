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
using DataAccess.EntitySpecification.NewsSpecification;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class NewsManager : INewsService
    {
        private readonly INewsDal newsDal;
        private readonly IMapper mapper;
        private readonly INewsSubScreenDal newsSubScreenDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        public NewsManager(INewsDal newsDal, IMapper mapper, INewsSubScreenDal newsSubScreenDal, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.newsSubScreenDal = newsSubScreenDal;
            this.mapper = mapper;
            this.newsDal = newsDal;

        }

        [SecuredOperation("Sudo,News.Create,News.All", Priority = 1)]
        [ValidationAspect(typeof(NewsValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<NewsForReturnDto> Create(NewsForCreationDto creationDto)
        {
            var checkByNameFromRepo = await newsDal.GetAsync(x => x.Header.ToLower() == creationDto.Header.ToLower());
            if (checkByNameFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            var mapForCreate = mapper.Map<News>(creationDto);
            var slideId = Guid.NewGuid();
            mapForCreate.SlideId = slideId;
            mapForCreate.UserId = claimId;
            mapForCreate.Created = DateTime.Now;
            mapForCreate.AnnounceType = "news";

            var createNews = await newsDal.Add(mapForCreate);
            var spec = new NewsWithUserSpecification(createNews.Id);

            var getNewsFromRepo = await newsDal.GetEntityWithSpecAsync(spec);
            return mapper.Map<News, NewsForReturnDto>(getNewsFromRepo);
        }

        [SecuredOperation("Sudo,News.Delete,News.All", Priority = 1)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<NewsForReturnDto> Delete(int Id)
        {
            var getByIdFromRepo = await newsDal.GetAsync(x => x.Id == Id);
            if (getByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await newsDal.Delete(getByIdFromRepo);
            return mapper.Map<News, NewsForReturnDto>(getByIdFromRepo);
        }



        //[SecuredOperation("Sudo,News.List,News.All", Priority = 1)]
        public async Task<Pagination<NewsForReturnDto>> GetListAsync(NewsParams queryParams)
        {
            var spec = new NewsWithPagingSpecification(queryParams);
            var listFromRepo = await newsDal.ListEntityWithSpecAsync(spec);
            var countSpec = new NewsWithFilterForCountAsyncSpecificaiton(queryParams);
            var totalItem = await newsDal.CountAsync(countSpec);

           
            var data = mapper.Map<List<News>, List<NewsForReturnDto>>(listFromRepo);
            return new Pagination<NewsForReturnDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItem,
                data
            );
        }

        [SecuredOperation("Sudo,News.Publish,News.All", Priority = 1)]
        [ValidationAspect(typeof(NewsValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<NewsForReturnDto> Publish(NewsForCreationDto updateDto)
        {
            var checkFromRepo = await newsDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var checkHomeNewsSubScreenForPublish = await newsSubScreenDal.GetListAsync(x => x.NewsId == updateDto.Id);
            if (checkHomeNewsSubScreenForPublish.Count <= 0)
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
            mapForUpdate.AnnounceType = "news";
            await newsDal.Update(mapForUpdate);

            var spec = new NewsWithUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await newsDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<News, NewsForReturnDto>(getAnnounceWithUserFromRepo);
        }

        [SecuredOperation("Sudo,News.Update,News.All", Priority = 1)]
        [ValidationAspect(typeof(NewsValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<NewsForReturnDto> Update(NewsForCreationDto updateDto)
        {
            var checkFromRepo = await newsDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromRepo);
            mapForUpdate.Updated = DateTime.Now;
            mapForUpdate.AnnounceType = "news";
            await newsDal.Update(mapForUpdate);

            var spec = new NewsWithUserSpecification(updateDto.Id);
            var getAnnounceWithUserFromRepo = await newsDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<News, NewsForReturnDto>(getAnnounceWithUserFromRepo);
        }
    }
}
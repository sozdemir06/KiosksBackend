using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Core.Entities.Concrete;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.NewsSubScreenSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class NewsSubScreenManager : INewsSubScreenService
    {
        private readonly INewsSubScreenDal newsSubScreenDal;
        private readonly IMapper mapper;
        private readonly IScreenDal screenDal;
        private readonly INewsDal newsDal;
        private readonly ISubSCreenDal subSCreenDal;
        public NewsSubScreenManager(INewsSubScreenDal newsSubScreenDal, INewsDal newsDal, IScreenDal screenDal, ISubSCreenDal subSCreenDal,
         IMapper mapper)
        {
            this.subSCreenDal = subSCreenDal;
            this.screenDal = screenDal;
            this.newsDal = newsDal;
            this.mapper = mapper;
            this.newsSubScreenDal = newsSubScreenDal;

        }

        [SecuredOperation("Sudo,NewsSubScreens.Create,News.All", Priority = 1)]
        [ValidationAspect(typeof(NewsSubScreenValidator), Priority = 2)]
        public async Task<NewsSubScreenForReturnDto> Create(NewsSubScreenForCreationDto creationDto)
        {
            var checkById = await newsSubScreenDal.GetAsync(x => x.SubScreenId == creationDto.SubScreenId && x.NewsId==creationDto.NewsId);
            if (checkById != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.SubScreenAlreadyExist });
            }

            var subScreenFromRepo = await subSCreenDal.GetAsync(x => x.Id == creationDto.SubScreenId);
            if (subScreenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundSubSCreen });

            }

            var checkAnnounceFromRepo = await newsDal.GetAsync(x => x.Id == creationDto.NewsId);
            if (checkAnnounceFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            var screenFromRepo = await screenDal.GetAsync(x => x.Id == creationDto.ScreenId);
            if (screenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundScreen });
            }

            var subScreenForReturn = new NewsSubScreen()
            {
                SubScreenId = subScreenFromRepo.Id,
                ScreenId = screenFromRepo.Id,
                NewsId = checkAnnounceFromRepo.Id,
                SubScreenName = subScreenFromRepo.Name,
                SubScreenPosition = subScreenFromRepo.Position

            };

            var createSubScreen = await newsSubScreenDal.Add(subScreenForReturn);
            var spec = new NewsSubScreenWithSubScreenForReturnSpecification(createSubScreen.Id);
            var getFromRepo = await newsSubScreenDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<NewsSubScreen, NewsSubScreenForReturnDto>(getFromRepo);
        }

        [SecuredOperation("Sudo,NewsSubScreens.Delete,News.All", Priority = 1)]
        public async Task<NewsSubScreenForReturnDto> Delete(int Id)
        {
             var checkByIdFromRepo = await newsSubScreenDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await newsSubScreenDal.Delete(checkByIdFromRepo);
            return mapper.Map<NewsSubScreen, NewsSubScreenForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,NewsSubScreens.List,News.All", Priority = 1)]
        public async Task<List<NewsSubScreenForReturnDto>> GetByAnnounceId(int announceId)
        {
             var spec = new NewsSubScreenWithSubScreenByNewsId(announceId);
            var getHomeAnnounceSubScreenByAnnounceId = await newsSubScreenDal.ListEntityWithSpecAsync(spec);
            if (getHomeAnnounceSubScreenByAnnounceId == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<NewsSubScreen>, List<NewsSubScreenForReturnDto>>(getHomeAnnounceSubScreenByAnnounceId);
        }

        [SecuredOperation("Sudo,NewsSubScreens.List,News.All", Priority = 1)]
        public async Task<List<NewsSubScreenForReturnDto>> GetListAsync()
        {
             var getListFromRepo = await newsSubScreenDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<NewsSubScreen>, List<NewsSubScreenForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,NewsSubScreens.Update,News.All", Priority = 1)]
        [ValidationAspect(typeof(NewsSubScreenValidator), Priority = 2)]
        public async  Task<NewsSubScreenForReturnDto> Update(NewsSubScreenForCreationDto updateDto)
        {
             var checkByIdFromRepo = await newsSubScreenDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await newsSubScreenDal.Update(mapForUpdate);
            return mapper.Map<NewsSubScreen,NewsSubScreenForReturnDto>(updatePhoto);
        }
    }
}
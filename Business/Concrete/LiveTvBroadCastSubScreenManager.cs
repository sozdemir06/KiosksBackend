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
using DataAccess.EntitySpecification.LiveTvBroadCastSubSCreenSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class LiveTvBroadCastSubScreenManager : ILiveTvBroadCastSubScreenService
    {
        private readonly ILiveTvBroadCastDal liveTvBroadCastDal;
        private readonly IMapper mapper;
        private readonly ISubSCreenDal subSCreenDal;
        private readonly ILiveTvBroadCastSubScreenDal liveTvBroadCastSubScreenDal;
        private readonly IScreenDal screenDal;
        public LiveTvBroadCastSubScreenManager(ILiveTvBroadCastDal liveTvBroadCastDal,
        IMapper mapper, ISubSCreenDal subSCreenDal,
        IScreenDal screenDal,
        ILiveTvBroadCastSubScreenDal liveTvBroadCastSubScreenDal)
        {
            this.screenDal = screenDal;
            this.liveTvBroadCastSubScreenDal = liveTvBroadCastSubScreenDal;
            this.mapper = mapper;
            this.subSCreenDal = subSCreenDal;
            this.liveTvBroadCastDal = liveTvBroadCastDal;

        }

        [SecuredOperation("Sudo,LiveTvBroadCasts.Create,LiveTvBroadCasts.All", Priority = 1)]
        [ValidationAspect(typeof(LiveTvBroadCastSubsCreenValidator), Priority = 2)]
        public async Task<LiveTvBroadCastSubScreenForReturnDto> Create(LiveTvBroadCastSubScreenForCreationDto creationDto)
        {
            var checkById = await liveTvBroadCastSubScreenDal.GetAsync(x => x.SubScreenId == creationDto.SubScreenId && x.LiveTvBroadCastId == creationDto.LiveTvBroadCastId);
            if (checkById != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.SubScreenAlreadyExist });
            }

            var subScreenFromRepo = await subSCreenDal.GetAsync(x => x.Id == creationDto.SubScreenId);
            if (subScreenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundSubSCreen });

            }

            var checkAnnounceFromRepo = await liveTvBroadCastDal.GetAsync(x => x.Id == creationDto.LiveTvBroadCastId);
            if (checkAnnounceFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            var screenFromRepo = await screenDal.GetAsync(x => x.Id == creationDto.ScreenId);
            if (screenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundScreen });
            }

            var subScreenForReturn = new LiveTvBroadCastSubScreen()
            {
                SubScreenId = subScreenFromRepo.Id,
                ScreenId = screenFromRepo.Id,
                LiveTvBroadCastId = checkAnnounceFromRepo.Id,
                SubScreenName = subScreenFromRepo.Name,
                SubScreenPosition = subScreenFromRepo.Position

            };

            var createSubScreen = await liveTvBroadCastSubScreenDal.Add(subScreenForReturn);
            var spec = new LiveTvBroadCastSubScreenWithSubScreenForReturnSpecification(createSubScreen.Id);
            var getFromRepo = await liveTvBroadCastSubScreenDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<LiveTvBroadCastSubScreen, LiveTvBroadCastSubScreenForReturnDto>(getFromRepo);
        }

        [SecuredOperation("Sudo,LiveTvBroadCasts.Delete,LiveTvBroadCasts.All", Priority = 1)]
        public async Task<LiveTvBroadCastSubScreenForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await liveTvBroadCastSubScreenDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await liveTvBroadCastSubScreenDal.Delete(checkByIdFromRepo);
            return mapper.Map<LiveTvBroadCastSubScreen, LiveTvBroadCastSubScreenForReturnDto>(checkByIdFromRepo);
        }

       [SecuredOperation("Sudo,LiveTvBroadCasts.List,LiveTvBroadCasts.All", Priority = 1)]
        public async Task<List<LiveTvBroadCastSubScreenForReturnDto>> GetByAnnounceId(int announceId)
        {
            var spec = new LiveTvBroadCastSubScreenWithSubScreenSpecification(announceId);
            var getHomeAnnounceSubScreenByAnnounceId = await liveTvBroadCastSubScreenDal.ListEntityWithSpecAsync(spec);
            if (getHomeAnnounceSubScreenByAnnounceId == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<LiveTvBroadCastSubScreen>, List<LiveTvBroadCastSubScreenForReturnDto>>(getHomeAnnounceSubScreenByAnnounceId);
        }

        [SecuredOperation("Sudo,LiveTvBroadCasts.List,LiveTvBroadCasts.All", Priority = 1)]
        public async Task<List<LiveTvBroadCastSubScreenForReturnDto>> GetListAsync()
        {
            var getListFromRepo = await liveTvBroadCastSubScreenDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<LiveTvBroadCastSubScreen>, List<LiveTvBroadCastSubScreenForReturnDto>>(getListFromRepo);
        }

       [SecuredOperation("Sudo,LiveTvBroadCasts.Update,LiveTvBroadCasts.All", Priority = 1)]
        [ValidationAspect(typeof(LiveTvBroadCastSubsCreenValidator), Priority = 2)]
        public async Task<LiveTvBroadCastSubScreenForReturnDto> Update(LiveTvBroadCastSubScreenForCreationDto updateDto)
        {
            var checkByIdFromRepo = await liveTvBroadCastSubScreenDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await liveTvBroadCastSubScreenDal.Update(mapForUpdate);
            return mapper.Map<LiveTvBroadCastSubScreen, LiveTvBroadCastSubScreenForReturnDto>(updatePhoto);
        }
    }
}
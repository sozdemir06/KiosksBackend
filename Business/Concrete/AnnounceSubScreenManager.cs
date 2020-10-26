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
using DataAccess.EntitySpecification.AnnounceSubScreenSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AnnounceSubScreenManager : IAnnounceSubScreenService
    {
        private readonly IAnnounceSubScreenDal announceSubScreenDal;
        private readonly IMapper mapper;
        private readonly IAnnounceDal announceDal;
        private readonly IScreenDal screenDal;
        private readonly ISubSCreenDal subSCreenDal;
        public AnnounceSubScreenManager(IAnnounceSubScreenDal announceSubScreenDal,
            IScreenDal screenDal, ISubSCreenDal subSCreenDal,
         IMapper mapper, IAnnounceDal announceDal)
        {
            this.subSCreenDal = subSCreenDal;
            this.screenDal = screenDal;
            this.announceDal = announceDal;
            this.mapper = mapper;
            this.announceSubScreenDal = announceSubScreenDal;

        }

        [SecuredOperation("Sudo,Announces.Create,Announces.All", Priority = 1)]
        [ValidationAspect(typeof(AnnouncesubScreenValidator), Priority = 2)]
        public async Task<AnnounceSubScreenForReturnDto> Create(AnnounceSubScreenForCreationDto creationDto)
        {
            var checkById = await announceSubScreenDal.GetAsync(x => x.SubScreenId == creationDto.SubScreenId && x.AnnounceId==creationDto.AnnounceId);
            if (checkById != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.SubScreenAlreadyExist });
            }

            var subScreenFromRepo = await subSCreenDal.GetAsync(x => x.Id == creationDto.SubScreenId);
            if (subScreenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundSubSCreen });

            }

            var checkAnnounceFromRepo = await announceDal.GetAsync(x => x.Id == creationDto.AnnounceId);
            if (checkAnnounceFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            var screenFromRepo = await screenDal.GetAsync(x => x.Id == creationDto.ScreenId);
            if (screenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundScreen });
            }

            if(!checkAnnounceFromRepo.IsPublish)
            {
               throw new RestException(HttpStatusCode.BadRequest, new { NotFound = "Duyuru henüz onay bekliyor...." });  
            }

            var subScreenForReturn = new AnnounceSubScreen()
            {
                SubScreenId = subScreenFromRepo.Id,
                ScreenId = screenFromRepo.Id,
                AnnounceId = checkAnnounceFromRepo.Id,
                SubScreenName = subScreenFromRepo.Name,
                SubScreenPosition = subScreenFromRepo.Position

            };

            var createSubScreen = await announceSubScreenDal.Add(subScreenForReturn);
            var spec = new AnnounSubScreenWithSubScreenForReturnSpecification(createSubScreen.Id);
            var getFromRepo = await announceSubScreenDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<AnnounceSubScreen, AnnounceSubScreenForReturnDto>(getFromRepo);
        }

        [SecuredOperation("Sudo,Announces.Delete,Announces.All", Priority = 1)]
        public async Task<AnnounceSubScreenForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await announceSubScreenDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await announceSubScreenDal.Delete(checkByIdFromRepo);
            return mapper.Map<AnnounceSubScreen, AnnounceSubScreenForReturnDto>(checkByIdFromRepo);
        }

       [SecuredOperation("Sudo,Announces.List,Announces.All", Priority = 1)]
        public async Task<List<AnnounceSubScreenForReturnDto>> GetByAnnounceId(int announceId)
        {
            var spec = new AnnounSubScreenWithSubScreenSpecification(announceId);
            var getHomeAnnounceSubScreenByAnnounceId = await announceSubScreenDal.ListEntityWithSpecAsync(spec);
            if (getHomeAnnounceSubScreenByAnnounceId == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<AnnounceSubScreen>, List<AnnounceSubScreenForReturnDto>>(getHomeAnnounceSubScreenByAnnounceId);
        }

        [SecuredOperation("Sudo,Announces.List,Announces.All", Priority = 1)]
        public async Task<List<AnnounceSubScreenForReturnDto>> GetListAsync()
        {
            var getListFromRepo = await announceSubScreenDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<AnnounceSubScreen>, List<AnnounceSubScreenForReturnDto>>(getListFromRepo);
        }

         [SecuredOperation("Sudo,Announces.Update,Announces.All", Priority = 1)]
        [ValidationAspect(typeof(AnnouncesubScreenValidator), Priority = 2)]
        public async Task<AnnounceSubScreenForReturnDto> Update(AnnounceSubScreenForCreationDto updateDto)
        {
            var checkByIdFromRepo = await announceSubScreenDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await announceSubScreenDal.Update(mapForUpdate);
            return mapper.Map<AnnounceSubScreen,AnnounceSubScreenForReturnDto>(updatePhoto);
        }
    }
}
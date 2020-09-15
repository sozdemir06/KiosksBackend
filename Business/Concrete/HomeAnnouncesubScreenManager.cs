
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
using DataAccess.EntitySpecification.HomeAnnounceSubScreenSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class HomeAnnouncesubScreenManager : IHomeAnnounceSubScreenService
    {
        private readonly IHomeAnnounceSubScreenDal homeAnnounceSubScreenDal;
        private readonly IMapper mapper;
        private readonly IHomeAnnounceDal homeAnnounceDal;
        private readonly IScreenDal screenDal;
        private readonly ISubSCreenDal subSCreenDal;

        public HomeAnnouncesubScreenManager(IHomeAnnounceSubScreenDal homeAnnounceSubScreenDal, 
            IMapper mapper, 
            IHomeAnnounceDal homeAnnounceDal,IScreenDal screenDal,ISubSCreenDal subSCreenDal
         
            )
        {
            this.homeAnnounceDal = homeAnnounceDal;
            this.screenDal = screenDal;
            this.subSCreenDal = subSCreenDal;
            this.mapper = mapper;
            this.homeAnnounceSubScreenDal = homeAnnounceSubScreenDal;
        

        }

        [SecuredOperation("Sudo,HomeAnnounces.Create,HomeAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceSubsCreenValidator), Priority = 2)]
        public async Task<HomeAnnounceSubScreenForReturnDto> Create(HomeAnnounceSubScreenForCreationDto creationDto)
        {
            var checkById = await homeAnnounceSubScreenDal.GetAsync(x => x.SubScreenId == creationDto.SubScreenId && x.HomeAnnounceId==creationDto.HomeAnnounceId);
            if (checkById != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.SubScreenAlreadyExist });
            }

            var subScreenFromRepo=await subSCreenDal.GetAsync(x=>x.Id==creationDto.SubScreenId);
            if(subScreenFromRepo==null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundSubSCreen });
 
            }

            var checkAnnounceFromRepo = await homeAnnounceDal.GetAsync(x=>x.Id==creationDto.HomeAnnounceId);
            if(checkAnnounceFromRepo==null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            var screenFromRepo= await screenDal.GetAsync(x=>x.Id==creationDto.ScreenId);
            if(screenFromRepo==null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundScreen });
            }

            var subScreenForReturn=new HomeAnnounceSubScreen()
            {
                SubScreenId=subScreenFromRepo.Id,
                ScreenId=screenFromRepo.Id,
                HomeAnnounceId=checkAnnounceFromRepo.Id,
                SubScreenName=subScreenFromRepo.Name,
                SubScreenPosition=subScreenFromRepo.Position
                
            };

            var createSubScreen = await homeAnnounceSubScreenDal.Add(subScreenForReturn);
            var spec=new HomeAnnounSubScreenWithSubScreenForReturnSpecification(createSubScreen.Id);
            var getFromRepo=await homeAnnounceSubScreenDal.GetEntityWithSpecAsync(spec);
            
            return mapper.Map<HomeAnnounceSubScreen, HomeAnnounceSubScreenForReturnDto>(getFromRepo);
        }

        [SecuredOperation("Sudo,HomeAnnounces.Delete,HomeAnnounces.All", Priority = 1)]
        public async Task<HomeAnnounceSubScreenForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await homeAnnounceSubScreenDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await homeAnnounceSubScreenDal.Delete(checkByIdFromRepo);
            return mapper.Map<HomeAnnounceSubScreen, HomeAnnounceSubScreenForReturnDto>(checkByIdFromRepo);
        }

         [SecuredOperation("Sudo,HomeAnnounces.List,HomeAnnounces.All", Priority = 1)]
        public async Task<List<HomeAnnounceSubScreenForReturnDto>> GetByAnnounceId(int announceId)
        {
              var spec=new HomeAnnounSubScreenWithSubScreenSpecification(announceId);
              var getHomeAnnounceSubScreenByAnnounceId=await homeAnnounceSubScreenDal.ListEntityWithSpecAsync(spec);
              if(getHomeAnnounceSubScreenByAnnounceId==null)
              {
                  throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound }); 
              }

              return mapper.Map<List<HomeAnnounceSubScreen>,List<HomeAnnounceSubScreenForReturnDto>>(getHomeAnnounceSubScreenByAnnounceId);

        }

        [SecuredOperation("Sudo,HomeAnnounces.List,HomeAnnounces.All", Priority = 1)]
        public async Task<List<HomeAnnounceSubScreenForReturnDto>> GetListAsync()
        {
            var getListFromRepo = await homeAnnounceSubScreenDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<HomeAnnounceSubScreen>, List<HomeAnnounceSubScreenForReturnDto>>(getListFromRepo);
        }

         [SecuredOperation("Sudo,HomeAnnounces.Update,HomeAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceSubsCreenValidator), Priority = 2)]
        public async Task<HomeAnnounceSubScreenForReturnDto> Update(HomeAnnounceSubScreenForCreationDto updateDto)
        {
            var checkByIdFromRepo = await homeAnnounceSubScreenDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await homeAnnounceSubScreenDal.Update(mapForUpdate);
            return mapper.Map<HomeAnnounceSubScreen, HomeAnnounceSubScreenForReturnDto>(updatePhoto);
        }
    }
}
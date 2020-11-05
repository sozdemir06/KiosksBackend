using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Logging;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using Core.Entities.Concrete;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.FoodMenuSubScreenSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class FoodMenuSubScreenManager : IFoodMenuSubScreenService
    {
        private readonly IFoodMenuSubScreenDal foodMenuSubScreenDal;
        private readonly IMapper mapper;
        private readonly IScreenDal screenDal;
        private readonly ISubSCreenDal subSCreenDal;
        private readonly IFoodMenuDal foodMenuDal;
        public FoodMenuSubScreenManager(IFoodMenuSubScreenDal foodMenuSubScreenDal, IMapper mapper,
        IFoodMenuDal foodMenuDal, IScreenDal screenDal, ISubSCreenDal subSCreenDal)
        {
            this.foodMenuDal = foodMenuDal;
            this.subSCreenDal = subSCreenDal;
            this.screenDal = screenDal;
            this.mapper = mapper;
            this.foodMenuSubScreenDal = foodMenuSubScreenDal;

        }

        [SecuredOperation("Sudo,FoodMenu.Create,FoodMenu.All", Priority = 1)]
        [ValidationAspect(typeof(FoodMenuSubScreenValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<FoodMenuSubScreenForReturnDto> Create(FoodMenuSubScreenForCreationDto creationDto)
        {
            var checkById = await foodMenuSubScreenDal.GetAsync(x => x.SubScreenId == creationDto.SubScreenId && x.FoodMenuId == creationDto.FoodMenuId);
            if (checkById != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.SubScreenAlreadyExist });
            }

            var subScreenFromRepo = await subSCreenDal.GetAsync(x => x.Id == creationDto.SubScreenId);
            if (subScreenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundSubSCreen });

            }

            var checkAnnounceFromRepo = await foodMenuDal.GetAsync(x => x.Id == creationDto.FoodMenuId);
            if (checkAnnounceFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            var screenFromRepo = await screenDal.GetAsync(x => x.Id == creationDto.ScreenId);
            if (screenFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundScreen });
            }

            var subScreenForReturn = new FoodMenuSubscreen()
            {
                SubScreenId = subScreenFromRepo.Id,
                ScreenId = screenFromRepo.Id,
                FoodMenuId = checkAnnounceFromRepo.Id,
                SubScreenName = subScreenFromRepo.Name,
                SubScreenPosition = subScreenFromRepo.Position

            };

            var createSubScreen = await foodMenuSubScreenDal.Add(subScreenForReturn);
            var spec = new FoodMenuSubScreenWithSubScreenForReturnSpecification(createSubScreen.Id);
            var getFromRepo = await foodMenuSubScreenDal.GetEntityWithSpecAsync(spec);

            return mapper.Map<FoodMenuSubscreen, FoodMenuSubScreenForReturnDto>(getFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenu.Delete,FoodMenu.All", Priority = 1)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<FoodMenuSubScreenForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await foodMenuSubScreenDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await foodMenuSubScreenDal.Delete(checkByIdFromRepo);
            return mapper.Map<FoodMenuSubscreen, FoodMenuSubScreenForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenu.List,FoodMenu.All", Priority = 1)]
        public async Task<List<FoodMenuSubScreenForReturnDto>> GetByAnnounceId(int announceId)
        {
            var spec = new FoodMenuSubScreenWithSubScreenSpecification(announceId);
            var getHomeAnnounceSubScreenByAnnounceId = await foodMenuSubScreenDal.ListEntityWithSpecAsync(spec);
            if (getHomeAnnounceSubScreenByAnnounceId == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<FoodMenuSubscreen>, List<FoodMenuSubScreenForReturnDto>>(getHomeAnnounceSubScreenByAnnounceId);
        }

        [SecuredOperation("Sudo,FoodMenu.List,FoodMenu.All", Priority = 1)]
        public async Task<List<FoodMenuSubScreenForReturnDto>> GetListAsync()
        {
            var getListFromRepo = await foodMenuSubScreenDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<FoodMenuSubscreen>, List<FoodMenuSubScreenForReturnDto>>(getListFromRepo);
        }


        [SecuredOperation("Sudo,FoodMenu.Update,FoodMenu.All", Priority = 1)]
        [ValidationAspect(typeof(FoodMenuSubScreenValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<FoodMenuSubScreenForReturnDto> Update(FoodMenuSubScreenForCreationDto updateDto)
        {
            var checkByIdFromRepo = await foodMenuSubScreenDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await foodMenuSubScreenDal.Update(mapForUpdate);
            return mapper.Map<FoodMenuSubscreen, FoodMenuSubScreenForReturnDto>(updatePhoto);
        }
    }
}
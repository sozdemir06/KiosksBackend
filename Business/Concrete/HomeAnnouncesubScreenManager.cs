
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
using Entities.Dtos;

namespace Business.Concrete
{
    public class HomeAnnouncesubScreenManager : IHomeAnnounceSubScreenService
    {
        private readonly IHomeAnnounceSubScreenDal homeAnnounceSubScreenDal;
        private readonly IMapper mapper;
        public HomeAnnouncesubScreenManager(IHomeAnnounceSubScreenDal homeAnnounceSubScreenDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.homeAnnounceSubScreenDal = homeAnnounceSubScreenDal;

        }

        [SecuredOperation("Sudo,HomeAnnounceSubScreens.Create", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnounceSubsCreenValidator), Priority = 2)]
        public async Task<HomeAnnounceSubScreenForReturnDto> Create(HomeAnnounceSubScreenForCreationDto creationDto)
        {
            var checkByName = await homeAnnounceSubScreenDal.GetAsync(x => x.SubScreenId == creationDto.SubScreenId);
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<HomeAnnounceSubScreen>(creationDto);
            var createPhoto = await homeAnnounceSubScreenDal.Add(mapForCreate);
            return mapper.Map<HomeAnnounceSubScreen, HomeAnnounceSubScreenForReturnDto>(createPhoto);
        }

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

        public async Task<List<HomeAnnounceSubScreenForReturnDto>> GetListAsync()
        {
            var getListFromRepo = await homeAnnounceSubScreenDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<HomeAnnounceSubScreen>, List<HomeAnnounceSubScreenForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,HomeAnnounceSubScreens.Update", Priority = 1)]
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
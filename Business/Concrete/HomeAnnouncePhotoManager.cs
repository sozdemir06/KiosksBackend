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
    public class HomeAnnouncePhotoManager : IHomeAnnouncePhotoService
    {
        private readonly IHomeAnnouncePhotoDal homeAnnouncePhotoDal;
        private readonly IMapper mapper;

        public HomeAnnouncePhotoManager(IHomeAnnouncePhotoDal homeAnnouncePhotoDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.homeAnnouncePhotoDal = homeAnnouncePhotoDal;

        }

        [SecuredOperation("Sudo,HomeAnnouncePhotos.Create", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnouncePhotoValidator), Priority = 2)]
        public async Task<HomeAnnouncePhotoForReturnDto> Create(HomeAnnouncePhotoForCreationDto creationDto)
        {
            var checkByName = await homeAnnouncePhotoDal.GetAsync(x => x.Name.ToLower().Contains(creationDto.Name.ToLower()));
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<HomeAnnouncePhoto>(creationDto);
            var createPhoto = await homeAnnouncePhotoDal.Add(mapForCreate);
            return mapper.Map<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,HomeAnnouncePhotos.Delete", Priority = 1)]
        public async Task<HomeAnnouncePhotoForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await homeAnnouncePhotoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await homeAnnouncePhotoDal.Delete(checkByIdFromRepo);
            return mapper.Map<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,HomeAnnouncePhotos.List", Priority = 1)]
        public async Task<List<HomeAnnouncePhotoForReturnDto>> GetListAsync()
        {
            var getListFromRepo = await homeAnnouncePhotoDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<HomeAnnouncePhoto>, List<HomeAnnouncePhotoForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,HomeAnnouncePhotos.Update", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnouncePhotoValidator), Priority = 2)]
        public async Task<HomeAnnouncePhotoForReturnDto> Update(HomeAnnouncePhotoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await homeAnnouncePhotoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await homeAnnouncePhotoDal.Update(mapForUpdate);
            return mapper.Map<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>(updatePhoto);
        }
    }
}
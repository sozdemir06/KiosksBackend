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
using Core.Utilities.Photos;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class HomeAnnouncePhotoManager : IHomeAnnouncePhotoService
    {
        private readonly IHomeAnnouncePhotoDal homeAnnouncePhotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        private readonly IHomeAnnounceDal homeAnnounceDal;

        public HomeAnnouncePhotoManager(IHomeAnnouncePhotoDal homeAnnouncePhotoDal,
                IMapper mapper, IUploadFile upload, IHomeAnnounceDal homeAnnounceDal)
        {
            this.homeAnnounceDal = homeAnnounceDal;
            this.upload = upload;
            this.mapper = mapper;
            this.homeAnnouncePhotoDal = homeAnnouncePhotoDal;

        }

        [SecuredOperation("Sudo,HomeAnnounces.Create,HomeAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnouncePhotoValidator), Priority = 2)]
        public async Task<HomeAnnouncePhotoForReturnDto> Create(FileUploadDto uploadDto)
        {

            var checkAnnounceById = await homeAnnounceDal.GetAsync(x => x.Id == uploadDto.AnnounceId);
            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.HomeAnnounceEmpty });
            }

            var uploadFile = await upload.Upload(uploadDto.File, "homeannounce");

            var mapForCreate = new HomeAnnouncePhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.HomeAnnounceId = uploadDto.AnnounceId;
            mapForCreate.IsConfirm = true;
            var mapForDb = mapper.Map<HomeAnnouncePhoto>(mapForCreate);
            var createPhoto = await homeAnnouncePhotoDal.Add(mapForDb);
            return mapper.Map<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,HomeAnnounces.Delete,HomeAnnounces.All", Priority = 1)]
        public async Task<HomeAnnouncePhotoForReturnDto> Delete(int Id)
        {

            var checkByIdFromRepo = await homeAnnouncePhotoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var deleteFileFromFolder = await upload.DeleteFile(checkByIdFromRepo.Name, "homeannounce");

            await homeAnnouncePhotoDal.Delete(checkByIdFromRepo);
            return mapper.Map<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,HomeAnnounces.List,HomeAnnounces.All", Priority = 1)]
        public async Task<List<HomeAnnouncePhotoForReturnDto>> GetListAsync(int announceId)
        {
            var getListFromRepo = await homeAnnouncePhotoDal.GetListAsync(x => x.HomeAnnounceId == announceId);
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<HomeAnnouncePhoto>, List<HomeAnnouncePhotoForReturnDto>>(getListFromRepo);
        }

         [SecuredOperation("Sudo,HomeAnnounces.Update,HomeAnnounces.All", Priority = 1)]
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
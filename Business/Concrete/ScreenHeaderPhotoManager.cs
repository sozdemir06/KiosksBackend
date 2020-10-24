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
    public class ScreenHeaderPhotoManager : IScreenHeaderPhotoService
    {
        private readonly IScreenHeaderPhotoDal screenHeaderPhotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        private readonly IScreenDal screenDal;
        public ScreenHeaderPhotoManager(IScreenHeaderPhotoDal screenHeaderPhotoDal, IMapper mapper, IUploadFile upload, IScreenDal screenDal)
        {
            this.screenDal = screenDal;
            this.upload = upload;
            this.mapper = mapper;
            this.screenHeaderPhotoDal = screenHeaderPhotoDal;


        }

        [SecuredOperation("Sudo,Screens.Create,Screens.All", Priority = 1)]
        [ValidationAspect(typeof(ScreenHeaderPhotoValidator), Priority = 2)]
        public async Task<ScreenHeaderPhotoForReturnDto> Create(FileUploadDto uploadDto)
        {
            var checkAnnounceById = await screenDal.GetAsync(x => x.Id == uploadDto.AnnounceId);
            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundScreen });
            }

            var uploadFile = await upload.Upload(uploadDto.File, "headerphoto");


            var mapForCreate = new ScreenHeaderPhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.ScreenId = checkAnnounceById.Id;
            mapForCreate.FileType = uploadFile.FileType;
            mapForCreate.IsMain = false;
            var mapForDb = mapper.Map<ScreenHeaderPhoto>(mapForCreate);
            var createPhoto = await screenHeaderPhotoDal.Add(mapForDb);
            return mapper.Map<ScreenHeaderPhoto, ScreenHeaderPhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,Screens.Delete,Screens.All", Priority = 1)]
        public async Task<ScreenHeaderPhotoForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await screenHeaderPhotoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var deleteFileFromFolder = await upload.DeleteFile(checkByIdFromRepo.Name, "headerphoto");

            await screenHeaderPhotoDal.Delete(checkByIdFromRepo);
            return mapper.Map<ScreenHeaderPhoto, ScreenHeaderPhotoForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,Screens.List,Screens.All", Priority = 1)]
        public async Task<List<ScreenHeaderPhotoForReturnDto>> GetListAsync(int announceId)
        {
            var getListFromRepo = await screenHeaderPhotoDal.GetListAsync(x => x.ScreenId == announceId);
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<ScreenHeaderPhoto>, List<ScreenHeaderPhotoForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,Screens.Update,Screens.All", Priority = 1)]
        [ValidationAspect(typeof(ScreenHeaderPhotoValidator), Priority = 2)]

        public async Task<ScreenHeaderPhotoForReturnDto> SetMain(ScreenHeaderPhotoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await screenHeaderPhotoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }
            var getAlreadyIsMain = await screenHeaderPhotoDal
                        .GetAsync(x => x.IsMain == true && x.Position.ToLower()==updateDto.Position.ToLower() && x.ScreenId==updateDto.ScreenId);
            if (getAlreadyIsMain != null)
            {
                getAlreadyIsMain.IsMain = false;
                await screenHeaderPhotoDal.Update(getAlreadyIsMain);
            }


            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await screenHeaderPhotoDal.Update(mapForUpdate);
            return mapper.Map<ScreenHeaderPhoto, ScreenHeaderPhotoForReturnDto>(updatePhoto);
        }

        [SecuredOperation("Sudo,Screens.Update,Screens.All", Priority = 1)]
        [ValidationAspect(typeof(ScreenHeaderPhotoValidator), Priority = 2)]
        public async Task<ScreenHeaderPhotoForReturnDto> Update(ScreenHeaderPhotoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await screenHeaderPhotoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            if(mapForUpdate.IsMain)
            {
                mapForUpdate.IsMain=false;
            }
            var updatePhoto = await screenHeaderPhotoDal.Update(mapForUpdate);
            return mapper.Map<ScreenHeaderPhoto, ScreenHeaderPhotoForReturnDto>(updatePhoto);
        }
    }
}
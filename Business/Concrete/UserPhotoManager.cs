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
    public class UserPhotoManager : IUserPhotoService
    {
        private readonly IUserPhotoDal userPotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        private readonly IUserDal userDal;
        public UserPhotoManager(IUserPhotoDal userPotoDal, IMapper mapper, IUploadFile upload, IUserDal userDal)
        {
            this.userDal = userDal;
            this.upload = upload;
            this.mapper = mapper;
            this.userPotoDal = userPotoDal;


        }
        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(UserPhotoValidator), Priority = 2)]
        public async Task<UserPhotoForReturnDto> Create(FileUploadDto uploadDto)
        {
            var checkAnnounceById = await userDal.GetAsync(x => x.Id == uploadDto.AnnounceId);
            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.UserNotFound });
            }

            var uploadFile = await upload.Upload(uploadDto.File, "userprofile");

            var mapForCreate = new UserPhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.UserId = uploadDto.AnnounceId;
            mapForCreate.IsConfirm = true;
            var mapForDb = mapper.Map<UserPhoto>(mapForCreate);
            var createPhoto = await userPotoDal.Add(mapForDb);
            return mapper.Map<UserPhoto, UserPhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<UserPhotoForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await userPotoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var deleteFileFromFolder = await upload.DeleteFile(checkByIdFromRepo.Name, "userprofile");

            await userPotoDal.Delete(checkByIdFromRepo);
            return mapper.Map<UserPhoto, UserPhotoForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<List<UserPhotoForReturnDto>> GetListAsync(int announceId)
        {
            var getListFromRepo = await userPotoDal.GetListAsync(x => x.UserId == announceId);
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<UserPhoto>, List<UserPhotoForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(UserPhotoValidator), Priority = 2)]
        public async Task<UserPhotoForReturnDto> Update(UserPhotoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await userPotoDal.GetAsync(x => x.Id == updateDto.UserId);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await userPotoDal.Update(mapForUpdate);
            return mapper.Map<UserPhoto, UserPhotoForReturnDto>(updatePhoto);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class AnnouncePhotoManager : IAnnouncePhotoService
    {
        private readonly IAnnouncePhotoDal announcePhotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        private readonly IAnnounceDal announceDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AnnouncePhotoManager(IAnnouncePhotoDal announcePhotoDal,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper, IUploadFile upload, IAnnounceDal announceDal)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.announceDal = announceDal;
            this.upload = upload;
            this.mapper = mapper;
            this.announcePhotoDal = announcePhotoDal;
        }

        [SecuredOperation("Sudo,Announces.Create,Announces.All", Priority = 1)]
        [ValidationAspect(typeof(AnnouncePhotoValidator), Priority = 2)]
        public async Task<AnnouncePhotoForReturnDto> Create(FileUploadDto uploadDto)
        {
            var checkAnnounceById = await announceDal.GetAsync(x => x.Id == uploadDto.AnnounceId);
            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            if (checkAnnounceById.ContentType.ToLower() != uploadDto.FileType.ToLower())
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = "Lütfen duyuru tipine uygun bir dosya yükleyin" });
            }

            var uploadFile = new UploadedFileResultDto();
            if (uploadDto.FileType.ToLower() == "image")
            {
                uploadFile = await upload.Upload(uploadDto.File, "announce");
            }

            if (uploadDto.FileType.ToLower() == "video")
            {
                uploadFile = await upload.UploadVideo(uploadDto.File, "announce");
            }

            var mapForCreate = new AnnouncePhotoForCretionDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.AnnounceId = uploadDto.AnnounceId;
            mapForCreate.FileType = uploadFile.FileType;
            mapForCreate.Duration = uploadDto.Duration;
            mapForCreate.IsConfirm = false;
            mapForCreate.UnConfirm = false;
            var mapForDb = mapper.Map<AnnouncePhoto>(mapForCreate);
            var createPhoto = await announcePhotoDal.Add(mapForDb);
            return mapper.Map<AnnouncePhoto, AnnouncePhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(AnnouncePhotoValidator), Priority = 2)]
        public async Task<AnnouncePhotoForReturnDto> CreateForPublic(FileUploadDto uploadDto)
        {
            var checkAnnounceById = await announceDal.GetAsync(x => x.Id == uploadDto.AnnounceId);

            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != checkAnnounceById.UserId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            if (checkAnnounceById.ContentType.ToLower() != uploadDto.FileType.ToLower())
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = "Lütfen duyuru tipine uygun bir dosya yükleyin" });
            }

            var uploadFile = new UploadedFileResultDto();
            if (uploadDto.FileType.ToLower() == "image")
            {
                uploadFile = await upload.Upload(uploadDto.File, "announce");
            }

            if (uploadDto.FileType.ToLower() == "video")
            {
                uploadFile = await upload.UploadVideo(uploadDto.File, "announce");
            }

            var mapForCreate = new AnnouncePhotoForCretionDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.AnnounceId = uploadDto.AnnounceId;
            mapForCreate.FileType = uploadFile.FileType;
            mapForCreate.Duration = uploadDto.Duration;
            mapForCreate.IsConfirm = false;
            mapForCreate.UnConfirm = false;
            var mapForDb = mapper.Map<AnnouncePhoto>(mapForCreate);
            var createPhoto = await announcePhotoDal.Add(mapForDb);

            //Make Announce IsNew True
            checkAnnounceById.IsNew = true;
            checkAnnounceById.IsPublish = false;
            checkAnnounceById.Reject = false;
            await announceDal.Update(checkAnnounceById);
            return mapper.Map<AnnouncePhoto, AnnouncePhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,Announces.Delete,Announces.All", Priority = 1)]
        public async Task<AnnouncePhotoForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await announcePhotoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var deleteFileFromFolder = await upload.DeleteFile(checkByIdFromRepo.Name, "announce");

            await announcePhotoDal.Delete(checkByIdFromRepo);
            return mapper.Map<AnnouncePhoto, AnnouncePhotoForReturnDto>(checkByIdFromRepo);
        }

        //[SecuredOperation("Sudo,Announces.List,Announces.All", Priority = 1)]
        public async Task<List<AnnouncePhotoForReturnDto>> GetListAsync(int announceId)
        {
            var getListFromRepo = await announcePhotoDal.GetListAsync(x => x.AnnounceId == announceId);
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<AnnouncePhoto>, List<AnnouncePhotoForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,Announces.Update,Announces.All", Priority = 1)]
        [ValidationAspect(typeof(AnnouncePhotoValidator), Priority = 2)]
        public async Task<AnnouncePhotoForReturnDto> Update(AnnouncePhotoForCretionDto updateDto)
        {
            var checkByIdFromRepo = await announcePhotoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await announcePhotoDal.Update(mapForUpdate);
            return mapper.Map<AnnouncePhoto, AnnouncePhotoForReturnDto>(updatePhoto);
        }
    }
}
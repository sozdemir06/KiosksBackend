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
using Core.Aspects.AutoFac.Logging;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Photos;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class HomeAnnouncePhotoManager : IHomeAnnouncePhotoService
    {
        private readonly IHomeAnnouncePhotoDal homeAnnouncePhotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        private readonly IHomeAnnounceDal homeAnnounceDal;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HomeAnnouncePhotoManager(IHomeAnnouncePhotoDal homeAnnouncePhotoDal,
        IHttpContextAccessor httpContextAccessor,
                IMapper mapper, IUploadFile upload, IHomeAnnounceDal homeAnnounceDal)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.homeAnnounceDal = homeAnnounceDal;
            this.upload = upload;
            this.mapper = mapper;
            this.homeAnnouncePhotoDal = homeAnnouncePhotoDal;

        }

        [SecuredOperation("Sudo,HomeAnnounces.Create,HomeAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnouncePhotoValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
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
            mapForCreate.IsConfirm = false;
            mapForCreate.UnConfirm = false;
            var mapForDb = mapper.Map<HomeAnnouncePhoto>(mapForCreate);
            var createPhoto = await homeAnnouncePhotoDal.Add(mapForDb);
            return mapper.Map<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(HomeAnnouncePhotoValidator), Priority = 2)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
        public async Task<HomeAnnouncePhotoForReturnDto> CreateForPublicAsync(FileUploadDto uploadDto)
        {
            var checkAnnounceById = await homeAnnounceDal.GetAsync(x => x.Id == uploadDto.AnnounceId);
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (claimId != checkAnnounceById.UserId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }

            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.HomeAnnounceEmpty });
            }

            var uploadFile = await upload.Upload(uploadDto.File, "homeannounce");

            var mapForCreate = new HomeAnnouncePhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.HomeAnnounceId = uploadDto.AnnounceId;
            mapForCreate.IsConfirm = false;
            var mapForDb = mapper.Map<HomeAnnouncePhoto>(mapForCreate);
            var createPhoto = await homeAnnouncePhotoDal.Add(mapForDb);
            return mapper.Map<HomeAnnouncePhoto, HomeAnnouncePhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,HomeAnnounces.Delete,HomeAnnounces.All", Priority = 1)]
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
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
        [LogAspect(typeof(PgSqlLogger), Priority = 3)]
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
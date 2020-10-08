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
    public class VehicleAnnouncePhotoManager : IVehicleAnnouncePhotoService
    {
        private readonly IVehicleAnnouncePhotoDal vehicleAnnouncePhotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        private readonly IVehicleAnnounceDal vehicleAnnounceDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        public VehicleAnnouncePhotoManager(IVehicleAnnouncePhotoDal vehicleAnnouncePhotoDal,
        IHttpContextAccessor httpContextAccessor,
                IMapper mapper, IUploadFile upload, IVehicleAnnounceDal vehicleAnnounceDal)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.vehicleAnnounceDal = vehicleAnnounceDal;
            this.upload = upload;
            this.mapper = mapper;
            this.vehicleAnnouncePhotoDal = vehicleAnnouncePhotoDal;

        }

        [SecuredOperation("Sudo,VehicleAnnounces.Create,VehicleAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleAnnouncePhotoValidator), Priority = 2)]
        public async Task<VehicleAnnouncePhotoForReturnDto> Create(FileUploadDto uploadDto)
        {
            var checkAnnounceById = await vehicleAnnounceDal.GetAsync(x => x.Id == uploadDto.AnnounceId);
            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            var uploadFile = await upload.Upload(uploadDto.File, "carannounce");

            var mapForCreate = new VehicleAnnouncePhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.VehicleAnnounceId = uploadDto.AnnounceId;
            mapForCreate.IsConfirm = false;
            mapForCreate.UnConfirm = false;
            var mapForDb = mapper.Map<VehicleAnnouncePhoto>(mapForCreate);
            var createPhoto = await vehicleAnnouncePhotoDal.Add(mapForDb);
            return mapper.Map<VehicleAnnouncePhoto, VehicleAnnouncePhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(VehicleAnnouncePhotoValidator), Priority = 2)]
        public async Task<VehicleAnnouncePhotoForReturnDto> CreateForPublicAsync(FileUploadDto uploadDto)
        {
            var checkAnnounceById = await vehicleAnnounceDal.GetAsync(x => x.Id == uploadDto.AnnounceId);
            var claimId = int.Parse(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            
            if (claimId != checkAnnounceById.UserId)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.OperationDenied });
            }
            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            var uploadFile = await upload.Upload(uploadDto.File, "carannounce");

            var mapForCreate = new VehicleAnnouncePhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.VehicleAnnounceId = uploadDto.AnnounceId;
            mapForCreate.IsConfirm = false;
            var mapForDb = mapper.Map<VehicleAnnouncePhoto>(mapForCreate);
            var createPhoto = await vehicleAnnouncePhotoDal.Add(mapForDb);
            return mapper.Map<VehicleAnnouncePhoto, VehicleAnnouncePhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,VehicleAnnounces.Delete,VehicleAnnounces.All", Priority = 1)]
        public async Task<VehicleAnnouncePhotoForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await vehicleAnnouncePhotoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var deleteFileFromFolder = await upload.DeleteFile(checkByIdFromRepo.Name, "carannounce");

            await vehicleAnnouncePhotoDal.Delete(checkByIdFromRepo);
            return mapper.Map<VehicleAnnouncePhoto, VehicleAnnouncePhotoForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,VehicleAnnounces.List,VehicleAnnounces.All", Priority = 1)]
        public async Task<List<VehicleAnnouncePhotoForReturnDto>> GetListAsync(int announceId)
        {
            var getListFromRepo = await vehicleAnnouncePhotoDal.GetListAsync(x => x.VehicleAnnounceId == announceId);
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<VehicleAnnouncePhoto>, List<VehicleAnnouncePhotoForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,VehicleAnnounces.Update,VehicleAnnounces.All", Priority = 1)]
        [ValidationAspect(typeof(VehicleAnnouncePhotoValidator), Priority = 2)]
        public async Task<VehicleAnnouncePhotoForReturnDto> Update(VehicleAnnouncePhotoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await vehicleAnnouncePhotoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await vehicleAnnouncePhotoDal.Update(mapForUpdate);
            return mapper.Map<VehicleAnnouncePhoto, VehicleAnnouncePhotoForReturnDto>(updatePhoto);
        }
    }
}
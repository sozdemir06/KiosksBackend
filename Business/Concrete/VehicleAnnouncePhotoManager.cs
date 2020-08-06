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
    public class VehicleAnnouncePhotoManager : IVehicleAnnouncePhotoService
    {
        private readonly IVehicleAnnouncePhotoDal vehicleAnnouncePhotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        private readonly IVehicleAnnounceDal vehicleAnnounceDal;
        public VehicleAnnouncePhotoManager(IVehicleAnnouncePhotoDal vehicleAnnouncePhotoDal,
                IMapper mapper, IUploadFile upload, IVehicleAnnounceDal vehicleAnnounceDal)
        {
            this.vehicleAnnounceDal = vehicleAnnounceDal;
            this.upload = upload;
            this.mapper = mapper;
            this.vehicleAnnouncePhotoDal = vehicleAnnouncePhotoDal;

        }

        [SecuredOperation("Sudo,VehicleAnnouncePhotos.Create,VehicleAnnounces.All", Priority = 1)]
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
            mapForCreate.IsConfirm = true;
            var mapForDb = mapper.Map<VehicleAnnouncePhoto>(mapForCreate);
            var createPhoto = await vehicleAnnouncePhotoDal.Add(mapForDb);
            return mapper.Map<VehicleAnnouncePhoto, VehicleAnnouncePhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,VehicleAnnouncePhotos.Delete,VehicleAnnounces.All", Priority = 1)]
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

        [SecuredOperation("Sudo,VehicleAnnouncePhotos.List,VehicleAnnounces.All", Priority = 1)]
        public async Task<List<VehicleAnnouncePhotoForReturnDto>> GetListAsync(int announceId)
        {
            var getListFromRepo = await vehicleAnnouncePhotoDal.GetListAsync(x => x.VehicleAnnounceId == announceId);
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<VehicleAnnouncePhoto>, List<VehicleAnnouncePhotoForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,VehicleAnnouncePhotos.Update,VehicleAnnounces.All", Priority = 1)]
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
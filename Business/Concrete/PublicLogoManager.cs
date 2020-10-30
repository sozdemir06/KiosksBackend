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
    public class PublicLogoManager : IPublicLogoService
    {
        private readonly IPublicLogoDal publicLogoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        public PublicLogoManager(IPublicLogoDal publicLogoDal, IMapper mapper, IUploadFile upload)
        {
            this.upload = upload;
            this.mapper = mapper;
            this.publicLogoDal = publicLogoDal;

        }

        [SecuredOperation("Sudo", Priority = 1)]
        [ValidationAspect(typeof(PublicLogoValidator), Priority = 2)]
        public async Task<PublicLogoForReturnDto> Create(FileUploadDto uploadDto)
        {

            var uploadFile = await upload.Upload(uploadDto.File, "publiclogo");


            var mapForCreate = new PublicLogoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.FileType = uploadFile.FileType;
            mapForCreate.IsMain = false;
            var mapForDb = mapper.Map<PublicLogo>(mapForCreate);
            var createPhoto = await publicLogoDal.Add(mapForDb);
            return mapper.Map<PublicLogo, PublicLogoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo", Priority = 1)]
        public async Task<PublicLogoForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await publicLogoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var deleteFileFromFolder = await upload.DeleteFile(checkByIdFromRepo.Name, "publiclogo");

            await publicLogoDal.Delete(checkByIdFromRepo);
            return mapper.Map<PublicLogo, PublicLogoForReturnDto>(checkByIdFromRepo);
        }

        public async Task<List<PublicLogoForReturnDto>> GetListAsync(int announceId)
        {
            var getListFromRepo = await publicLogoDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<PublicLogo>, List<PublicLogoForReturnDto>>(getListFromRepo);
        }


        public async Task<PublicLogoForReturnDto> GetMainLogo()
        {
             var mainLogo=await publicLogoDal.GetAsync(x=>x.IsMain==true);
             return mapper.Map<PublicLogo,PublicLogoForReturnDto>(mainLogo);
        }

        [SecuredOperation("Sudo", Priority = 1)]
        [ValidationAspect(typeof(PublicLogoValidator), Priority = 2)]
        public async Task<PublicLogoForReturnDto> SetMain(PublicLogoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await publicLogoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }
            var getAlreadyIsMain = await publicLogoDal
                        .GetAsync(x => x.IsMain == true);
            if (getAlreadyIsMain != null)
            {
                getAlreadyIsMain.IsMain = false;
                await publicLogoDal.Update(getAlreadyIsMain);
            }


            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await publicLogoDal.Update(mapForUpdate);
            return mapper.Map<PublicLogo, PublicLogoForReturnDto>(updatePhoto);
        }

        [SecuredOperation("Sudo", Priority = 1)]
        [ValidationAspect(typeof(PublicLogoValidator), Priority = 2)]
        public async Task<PublicLogoForReturnDto> Update(PublicLogoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await publicLogoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            if (mapForUpdate.IsMain)
            {
                mapForUpdate.IsMain = false;
            }
            var updatePhoto = await publicLogoDal.Update(mapForUpdate);
            return mapper.Map<PublicLogo, PublicLogoForReturnDto>(updatePhoto);
        }
    }
}
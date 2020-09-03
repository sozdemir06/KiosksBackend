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
    public class FoodMenuBgPhotoManager : IFoodMenuBgPhotoService
    {
        private readonly IFoodMenuBgPhotoDal foodMenuBgPhotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        public FoodMenuBgPhotoManager(IFoodMenuBgPhotoDal foodMenuBgPhotoDal, IMapper mapper, IUploadFile upload)
        {
            this.upload = upload;
            this.mapper = mapper;
            this.foodMenuBgPhotoDal = foodMenuBgPhotoDal;

        }

        [SecuredOperation("Sudo,FoodMenuPhotos.Create,FoodMenu.All", Priority = 1)]
        [ValidationAspect(typeof(FoodMenuBgPhotoValidator), Priority = 2)]
        public async Task<FoodMenuBgPhotoForReturnDto> Create(FileUploadDto uploadDto)
        {


            var uploadFile = await upload.Upload(uploadDto.File, "foodmenu");

            var mapForCreate = new FoodMenuBgPhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.IsSetBackground = false;
            var mapForDb = mapper.Map<FoodMenuBgPhoto>(mapForCreate);
            var createPhoto = await foodMenuBgPhotoDal.Add(mapForDb);
            return mapper.Map<FoodMenuBgPhoto, FoodMenuBgPhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,FoodMenuPhotos.Delete,FoodMenu.All", Priority = 1)]
        public async Task<FoodMenuBgPhotoForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await foodMenuBgPhotoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var deleteFileFromFolder = await upload.DeleteFile(checkByIdFromRepo.Name, "foodmenu");

            await foodMenuBgPhotoDal.Delete(checkByIdFromRepo);
            return mapper.Map<FoodMenuBgPhoto, FoodMenuBgPhotoForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenuPhotos.List,FoodMenu.All", Priority = 1)]
        public async Task<List<FoodMenuBgPhotoForReturnDto>> GetListAsync()
        {
            var getListFromRepo = await foodMenuBgPhotoDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<FoodMenuBgPhoto>, List<FoodMenuBgPhotoForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenuPhotos.Update,FoodMenu.All", Priority = 1)]
        [ValidationAspect(typeof(FoodMenuBgPhotoValidator), Priority = 2)]
        public async Task<FoodMenuBgPhotoForReturnDto> SetBackgroundPhoto(FoodMenuBgPhotoForCreationDto updateDto)
        {
             var checkByIdFromRepo = await foodMenuBgPhotoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var getCurrentSelectedBgPhoto=await foodMenuBgPhotoDal.GetAsync(x=>x.IsSetBackground==true);
            if(getCurrentSelectedBgPhoto!=null)
            {
                getCurrentSelectedBgPhoto.IsSetBackground=false;
                await foodMenuBgPhotoDal.Update(getCurrentSelectedBgPhoto);
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await foodMenuBgPhotoDal.Update(mapForUpdate);
            return mapper.Map<FoodMenuBgPhoto, FoodMenuBgPhotoForReturnDto>(updatePhoto);
        }

        [SecuredOperation("Sudo,FoodMenuPhotos.Update,FoodMenu.All", Priority = 1)]
        [ValidationAspect(typeof(FoodMenuBgPhotoValidator), Priority = 2)]
        public async Task<FoodMenuBgPhotoForReturnDto> Update(FoodMenuBgPhotoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await foodMenuBgPhotoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await foodMenuBgPhotoDal.Update(mapForUpdate);
            return mapper.Map<FoodMenuBgPhoto, FoodMenuBgPhotoForReturnDto>(updatePhoto);
        }
    }
}
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
    public class FoodMenuPhotoManager : IFoodMenuPhotoService
    {
        private readonly IFoodMenuPhotoDal foodMenuPhotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        private readonly IFoodMenuDal foodMenuDal;
        public FoodMenuPhotoManager(IFoodMenuPhotoDal foodMenuPhotoDal,
        IMapper mapper, IUploadFile upload,
        IFoodMenuDal foodMenuDal)
        {
            this.foodMenuDal = foodMenuDal;
            this.upload = upload;
            this.mapper = mapper;
            this.foodMenuPhotoDal = foodMenuPhotoDal;

        }

        [SecuredOperation("Sudo,FoodMenu.Create,FoodMenu.All", Priority = 1)]
        [ValidationAspect(typeof(FoodMenuPhotoValidator), Priority = 2)]
        public async Task<FoodMenuPhotoForReturnDto> Create(FileUploadDto uploadDto)
        {
            var checkAnnounceById = await foodMenuDal.GetAsync(x => x.Id == uploadDto.AnnounceId);
            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

            var uploadFile = await upload.Upload(uploadDto.File, "foodmenu");

            var mapForCreate = new FoodMenuPhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.FoodMenuId = uploadDto.AnnounceId;
            mapForCreate.FileType = uploadFile.FileType;
            mapForCreate.IsConfirm = false;
            mapForCreate.UnConfirm = true;
            var mapForDb = mapper.Map<FoodMenuPhoto>(mapForCreate);
            var createPhoto = await foodMenuPhotoDal.Add(mapForDb);
            return mapper.Map<FoodMenuPhoto, FoodMenuPhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,FoodMenu.Delete,FoodMenu.All", Priority = 1)]
        public async Task<FoodMenuPhotoForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await foodMenuPhotoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var deleteFileFromFolder = await upload.DeleteFile(checkByIdFromRepo.Name, "foodmenu");

            await foodMenuPhotoDal.Delete(checkByIdFromRepo);
            return mapper.Map<FoodMenuPhoto, FoodMenuPhotoForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenu.List,FoodMenu.All", Priority = 1)]
        public async Task<List<FoodMenuPhotoForReturnDto>> GetListAsync(int announceId)
        {
            var getListFromRepo = await foodMenuPhotoDal.GetListAsync(x => x.FoodMenuId == announceId);
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<FoodMenuPhoto>, List<FoodMenuPhotoForReturnDto>>(getListFromRepo);
        }

        [SecuredOperation("Sudo,FoodMenu.List,FoodMenu.All", Priority = 1)]
        public async Task<List<FoodMenuPhotoForReturnDto>> GetListBackgroundAsync()
        {
            var getListFromRepo = await foodMenuPhotoDal.GetListAsync();
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<FoodMenuPhoto>, List<FoodMenuPhotoForReturnDto>>(getListFromRepo);
        }


        [SecuredOperation("Sudo,FoodMenu.Update,FoodMenu.All", Priority = 1)]
        [ValidationAspect(typeof(FoodMenuPhotoValidator), Priority = 2)]
        public async Task<FoodMenuPhotoForReturnDto> Update(FoodMenuPhotoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await foodMenuPhotoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await foodMenuPhotoDal.Update(mapForUpdate);
            return mapper.Map<FoodMenuPhoto, FoodMenuPhotoForReturnDto>(updatePhoto);
        }

    }
}
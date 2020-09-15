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
    public class NewsPhotoManager : INewsPhotoService
    {
        private readonly INewsPhotoDal newsPhotoDal;
        private readonly IMapper mapper;
        private readonly IUploadFile upload;
        private readonly INewsDal newsDal;
        public NewsPhotoManager(INewsPhotoDal newsPhotoDal, IMapper mapper,
             IUploadFile upload, INewsDal newsDal)
        {
            this.newsDal = newsDal;
            this.upload = upload;
            this.mapper = mapper;
            this.newsPhotoDal = newsPhotoDal;

        }

        [SecuredOperation("Sudo,News.Create,News.All", Priority = 1)]
        [ValidationAspect(typeof(NewsPhotoValidator), Priority = 2)]
        public async Task<NewsPhotoForReturnDto> Create(FileUploadDto uploadDto)
        {
            var checkAnnounceById = await newsDal.GetAsync(x => x.Id == uploadDto.AnnounceId);
            if (checkAnnounceById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFoundAnnounce });
            }

           

            var uploadFile = new UploadedFileResultDto();
            if (uploadDto.FileType.ToLower() == "image")
            {
                uploadFile = await upload.Upload(uploadDto.File, "announce");
            }

            var mapForCreate = new NewsPhotoForCreationDto();
            mapForCreate.Name = uploadFile.Name;
            mapForCreate.FullPath = uploadFile.FullPath;
            mapForCreate.NewsId = uploadDto.AnnounceId;
            mapForCreate.FileType = uploadFile.FileType;
            mapForCreate.IsConfirm = true;
            var mapForDb = mapper.Map<NewsPhoto>(mapForCreate);
            var createPhoto = await newsPhotoDal.Add(mapForDb);
            return mapper.Map<NewsPhoto, NewsPhotoForReturnDto>(createPhoto);
        }

        [SecuredOperation("Sudo,News.Delete,News.All", Priority = 1)]
        public async Task<NewsPhotoForReturnDto> Delete(int Id)
        {
            var checkByIdFromRepo = await newsPhotoDal.GetAsync(x => x.Id == Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var deleteFileFromFolder = await upload.DeleteFile(checkByIdFromRepo.Name, "announce");

            await newsPhotoDal.Delete(checkByIdFromRepo);
            return mapper.Map<NewsPhoto, NewsPhotoForReturnDto>(checkByIdFromRepo);
        }

        [SecuredOperation("Sudo,News.List,News.All", Priority = 1)]
        public async Task<List<NewsPhotoForReturnDto>> GetListAsync(int announceId)
        {
            var getListFromRepo = await newsPhotoDal.GetListAsync(x => x.NewsId == announceId);
            if (getListFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            return mapper.Map<List<NewsPhoto>, List<NewsPhotoForReturnDto>>(getListFromRepo);
        }

         [SecuredOperation("Sudo,News.Update,News.All", Priority = 1)]
        [ValidationAspect(typeof(NewsPhotoValidator), Priority = 2)]
        public async Task<NewsPhotoForReturnDto> Update(NewsPhotoForCreationDto updateDto)
        {
            var checkByIdFromRepo = await newsPhotoDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkByIdFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkByIdFromRepo);
            var updatePhoto = await newsPhotoDal.Update(mapForUpdate);
            return mapper.Map<NewsPhoto, NewsPhotoForReturnDto>(updatePhoto);
        }
    }
}
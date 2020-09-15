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
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AnnounceContentTypeManager : IAnnounceContentTypeService
    {
        private readonly IAnnounceContentTypeDal announceConetntTypeDal;
        private readonly IMapper mapper;
        public AnnounceContentTypeManager(IAnnounceContentTypeDal announceConetntTypeDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.announceConetntTypeDal = announceConetntTypeDal;

        }


        [SecuredOperation("Sudo,AnnounceOptons.All", Priority = 1)]
        [ValidationAspect(typeof(AnnounceContentTypeValidator), Priority = 2)]

        public async Task<AnnounceContentTypeForReturnDto> Create(AnnounceContentTypeForCreationDto createDto)
        {
            var checkByName = await announceConetntTypeDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<AnnounceContentType>(createDto);
            var saveToDb = await announceConetntTypeDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<AnnounceContentType, AnnounceContentTypeForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,AnnounceOptons.All", Priority = 1)]
        public async Task<AnnounceContentTypeForReturnDto> Delete(int Id)
        {
            var checkFromDb = await announceConetntTypeDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await announceConetntTypeDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<AnnounceContentType, AnnounceContentTypeForReturnDto>(checkFromDb);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,AnnounceOptons.All", Priority = 1)]
        public async Task<List<AnnounceContentTypeForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await announceConetntTypeDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<AnnounceContentType>, List<AnnounceContentTypeForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

         [SecuredOperation("Sudo,AnnounceOptons.All", Priority = 1)]
        public async Task<AnnounceContentTypeForReturnDto> Update(AnnounceContentTypeForCreationDto updateDto)
        {
            var checkById = await announceConetntTypeDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await announceConetntTypeDal.Update(mapForUpdate);
            return mapper.Map<AnnounceContentType, AnnounceContentTypeForReturnDto>(mapForUpdate);
        }
    }
}
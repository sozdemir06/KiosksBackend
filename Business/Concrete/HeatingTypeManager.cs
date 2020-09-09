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
    public class HeatingTypeManager : IHeatingTypeService
    {
        private readonly IHeatingTypeDal heatingTypeDal;
        private readonly IMapper mapper;
        public HeatingTypeManager(IHeatingTypeDal heatingTypeDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.heatingTypeDal = heatingTypeDal;

        }

        [SecuredOperation("Sudo,HeatingTypes.Create", Priority = 1)]
        [ValidationAspect(typeof(HeatingTypeValidator), Priority = 2)]
        public async Task<HeatingTypeForReturnDto> Create(HeatingTypeForCreationDto createDto)
        {
            var checkByName = await heatingTypeDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<HeatingType>(createDto);
            var saveToDb = await heatingTypeDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<HeatingType, HeatingTypeForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,HeatingTypes.Delete", Priority = 1)]
        public async Task<HeatingTypeForReturnDto> Delete(int Id)
        {
            var checkFromDb = await heatingTypeDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await heatingTypeDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<HeatingType, HeatingTypeForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,HeatingTypes.List", Priority = 1)]
        public async Task<List<HeatingTypeForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await heatingTypeDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<HeatingType>, List<HeatingTypeForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,HeatingTypes.Update", Priority = 1)]
        [ValidationAspect(typeof(HeatingTypeValidator), Priority = 2)]
        public async Task<HeatingTypeForReturnDto> Update(HeatingTypeForCreationDto updateDto)
        {
            var checkById = await heatingTypeDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await heatingTypeDal.Update(mapForUpdate);
            return mapper.Map<HeatingType, HeatingTypeForReturnDto>(mapForUpdate);
        }
    }
}
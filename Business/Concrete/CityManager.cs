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
    public class CityManager : ICityService
    {
        private readonly ICityDal cityDal;
        private readonly IMapper mapper;
        public CityManager(ICityDal cityDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.cityDal = cityDal;

        }

        [SecuredOperation("Sudo,Cities.Create", Priority = 1)]
        [ValidationAspect(typeof(CityValidator), Priority = 2)]
        public async Task<CityForReturnDto> Create(CityForCreationDto createDto)
        {
            var checkByName = await cityDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<City>(createDto);
            var saveToDb = await cityDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<City, CityForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,Cities.Delete", Priority = 1)]
        public async Task<CityForReturnDto> Delete(int Id)
        {
            var checkFromDb = await cityDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await cityDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<City, CityForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,Cities.List", Priority = 1)]
        public async Task<List<CityForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await cityDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<City>, List<CityForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,Cities.Update", Priority = 1)]
        [ValidationAspect(typeof(CityValidator), Priority = 2)]
        public async Task<CityForReturnDto> Update(CityForCreationDto updateDto)
        {
           var checkById = await cityDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await cityDal.Update(mapForUpdate);
            return mapper.Map<City, CityForReturnDto>(mapForUpdate);
        }
    }
}
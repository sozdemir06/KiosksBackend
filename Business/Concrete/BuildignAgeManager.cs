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
    public class BuildignAgeManager : IBuildingageService
    {
        private readonly IBuildingAgeDal buildingAgeDal;

        private readonly IMapper mapper;
        public BuildignAgeManager(IBuildingAgeDal buildingAgeDal, IMapper mapper)
        {
            this.buildingAgeDal = buildingAgeDal;
            this.mapper = mapper;
        }

        [SecuredOperation("Sudo,BuildingsAge.Create", Priority = 1)]
        [ValidationAspect(typeof(BuildingAgeValidator), Priority = 2)]
        public async Task<BuildingAgeForReturnDto> Create(BuildingAgeForCretationDto createDto)
        {
            var checkByName = await buildingAgeDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<BuildingAge>(createDto);
            var saveToDb = await buildingAgeDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<BuildingAge, BuildingAgeForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,BuildingsAge.Delete", Priority = 1)]
        public async Task<BuildingAgeForReturnDto> Delete(int Id)
        {
            var checkFromDb = await buildingAgeDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await buildingAgeDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<BuildingAge, BuildingAgeForReturnDto>(checkFromDb);
            return mapForReturn;
        }


        [SecuredOperation("Sudo,BuildingsAge.List", Priority = 1)]
        public async Task<List<BuildingAgeForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await buildingAgeDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<BuildingAge>, List<BuildingAgeForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,BuildingsAge.Update", Priority = 1)]
        [ValidationAspect(typeof(BuildingAgeValidator), Priority = 2)]
        public async Task<BuildingAgeForReturnDto> Update(BuildingAgeForCretationDto updateDto)
        {
            var checkById = await buildingAgeDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await buildingAgeDal.Update(mapForUpdate);
            return mapper.Map<BuildingAge, BuildingAgeForReturnDto>(mapForUpdate);
        }
    }
}
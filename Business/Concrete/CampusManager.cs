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
    public class CampusManager : ICampusService
    {
        private readonly ICampusDal campusDal;
        private readonly IMapper mapper;
        public CampusManager(ICampusDal campusDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.campusDal = campusDal;

        }

        [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        [ValidationAspect(typeof(CampusValidator), Priority = 2)]
        public async Task<CampusForReturnDto> Create(CampuseForCreationDto createDto)
        {
            var checkByName = await campusDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<Campus>(createDto);
            var saveToDb = await campusDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<Campus, CampusForReturnDto>(saveToDb);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        public async Task<CampusForReturnDto> Delete(int Id)
        {
            var checkFromDb = await campusDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await campusDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<Campus, CampusForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        //[SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        public async Task<List<CampusForReturnDto>> GetCampusListAsync()
        {
            var campuses = await campusDal.GetListAsync();
            if (campuses == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { CampusNotFound = Messages.CampusListNotFound });
            }

            var campusForReturn = mapper.Map<List<Campus>, List<CampusForReturnDto>>(campuses);

            return campusForReturn;
        }

        [SecuredOperation("Sudo,UserOptions.All", Priority = 1)]
        [ValidationAspect(typeof(CampusValidator), Priority = 2)]
        public async Task<CampusForReturnDto> Update(CampuseForCreationDto updateDto)
        {
            var checkById = await campusDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await campusDal.Update(mapForUpdate);
            return mapper.Map<Campus, CampusForReturnDto>(mapForUpdate);
        }
    }
}
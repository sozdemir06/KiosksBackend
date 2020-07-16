using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidaitonRules;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Core.Entities.Concrete;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class FlatOfHomeManager : IFlatOfHomeService
    {
        private readonly IFlatOfHomeDal flatOFHomeDal;
        private readonly IMapper mapper;
        public FlatOfHomeManager(IFlatOfHomeDal flatOFHomeDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.flatOFHomeDal = flatOFHomeDal;

        }
        [SecuredOperation("Sudo,FlatsOfHome.Create", Priority = 1)]
        [ValidationAspect(typeof(FlatOfHomeValidator), Priority = 2)]
        public async Task<FlatOfHomeForReturnDto> Create(FlatOfHomeForCreationDto createDto)
        {
            var checkByName = await flatOFHomeDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<FlatOfHome>(createDto);
            var saveToDb = await flatOFHomeDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<FlatOfHome, FlatOfHomeForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,FlatsOfHome.Delete", Priority = 1)]
        public async Task<FlatOfHomeForReturnDto> Delete(int Id)
        {
            var checkFromDb = await flatOFHomeDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await flatOFHomeDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<FlatOfHome, FlatOfHomeForReturnDto>(checkFromDb);
            return mapForReturn;
        }


       [SecuredOperation("Sudo,FlatsOfHome.List", Priority = 1)]
        public async Task<List<FlatOfHomeForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await flatOFHomeDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<FlatOfHome>, List<FlatOfHomeForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,FlatsOfHome.Update", Priority = 1)]
        [ValidationAspect(typeof(FlatOfHomeValidator), Priority = 2)]
        public async Task<FlatOfHomeForReturnDto> Update(FlatOfHomeForCreationDto updateDto)
        {
            var checkById = await flatOFHomeDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await flatOFHomeDal.Update(mapForUpdate);
            return mapper.Map<FlatOfHome, FlatOfHomeForReturnDto>(mapForUpdate);
        }
    }
}
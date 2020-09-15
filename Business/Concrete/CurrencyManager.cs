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
    public class CurrencyManager : ICurrencyService
    {
        private readonly ICurrencyDal currencyDal;
        private readonly IMapper mapper;
        public CurrencyManager(ICurrencyDal currencyDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.currencyDal = currencyDal;

        }
        [SecuredOperation("Sudo,AddMoneyForExchangeRate", Priority = 1)]
        [ValidationAspect(typeof(CurrencyValidator), Priority = 2)]
        public async Task<CurrencyForReturnDto> Create(CurrencyForCreationDto createDto)
        {

            var checkByName = await currencyDal.GetAsync(x => x.Name.ToLower() == createDto.Name.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<Currency>(createDto);
            var saveToDb = await currencyDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<Currency, CurrencyForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,AddMoneyForExchangeRate", Priority = 1)]
        public async Task<CurrencyForReturnDto> Delete(int Id)
        {
            var checkFromDb = await currencyDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await currencyDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<Currency, CurrencyForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,AddMoneyForExchangeRate", Priority = 1)]
        public async Task<List<CurrencyForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await currencyDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<Currency>, List<CurrencyForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

         [SecuredOperation("Sudo,AddMoneyForExchangeRate", Priority = 1)]
        [ValidationAspect(typeof(CurrencyValidator), Priority = 2)]
        public async Task<CurrencyForReturnDto> Update(CurrencyForCreationDto updateDto)
        {
            var checkById = await currencyDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await currencyDal.Update(mapForUpdate);
            return mapper.Map<Currency, CurrencyForReturnDto>(mapForUpdate);
        }
    }
}
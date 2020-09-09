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
    public class ScreenFooterManager : IScreenFooterService
    {
        private readonly IScreenFooterDal screenFooterDal;
        private readonly IMapper mapper;

        public ScreenFooterManager(IScreenFooterDal screenFooterDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.screenFooterDal = screenFooterDal;

        }

        [SecuredOperation("Sudo,ScreenFooter.Create,Screen.All", Priority = 1)]
        [ValidationAspect(typeof(ScreenFooterValidator), Priority = 2)]
        public async Task<ScreenFooterForReturnDto> Create(ScreenFooterForCreationDto createDto)
        {
            var checkFromRepo = await screenFooterDal.GetAsync(x => x.FooterText.ToLower() == createDto.FooterText.ToLower());
            if (checkFromRepo != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.AlreadyExist });

            }

            var screenFooterForCreate = mapper.Map<ScreenFooter>(createDto);
            await screenFooterDal.Add(screenFooterForCreate);
            return mapper.Map<ScreenFooter, ScreenFooterForReturnDto>(screenFooterForCreate);
        }

        [SecuredOperation("Sudo,ScreenFooter.Delete,Screen.All", Priority = 1)]
        public async Task<ScreenFooterForReturnDto> Delete(int Id)
        {
            var checkFromDb = await screenFooterDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await screenFooterDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<ScreenFooter, ScreenFooterForReturnDto>(checkFromDb);
            return mapForReturn;
        }
        [SecuredOperation("Sudo,ScreenFooter.List,Screen.All", Priority = 1)]
        public async Task<List<ScreenFooterForReturnDto>> GetListAsync()
        {
            var numberOfRoomsList = await screenFooterDal.GetListAsync();
            if (numberOfRoomsList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<ScreenFooter>, List<ScreenFooterForReturnDto>>(numberOfRoomsList);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,ScreenHeader.Update,Screen.All", Priority = 1)]
        [ValidationAspect(typeof(ScreenHeaderValidator), Priority = 2)]
        public async Task<ScreenFooterForReturnDto> Update(ScreenFooterForCreationDto updateDto)
        {
            var checkFromDb = await screenFooterDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkFromDb);
            await screenFooterDal.Update(mapForUpdate);
            var mapForReturn = mapper.Map<ScreenFooter, ScreenFooterForReturnDto>(mapForUpdate);
            return mapForReturn;
        }
    }
}
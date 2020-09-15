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
    public class ScreenHeaderManager : IScreenHeaderService
    {
        private readonly IScreenHeaderDal screenHeaderDal;
        private readonly IMapper mapper;
        public ScreenHeaderManager(IScreenHeaderDal screenHeaderDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.screenHeaderDal = screenHeaderDal;

        }

        [SecuredOperation("Sudo,Screens.Create,Screens.All", Priority = 1)]
        [ValidationAspect(typeof(ScreenHeaderValidator), Priority = 2)]
        public async Task<ScreenHeaderForReturnDto> Create(ScreenHeaderForCreationDto createDto)
        {

            var screenHeaderForCreate = mapper.Map<ScreenHeader>(createDto);
            await screenHeaderDal.Add(screenHeaderForCreate);
            return mapper.Map<ScreenHeader, ScreenHeaderForReturnDto>(screenHeaderForCreate);
        }

       [SecuredOperation("Sudo,Screens.Delete,Screens.All", Priority = 1)]
        public async Task<ScreenHeaderForReturnDto> Delete(int Id)
        {
            var checkFromDb = await screenHeaderDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await screenHeaderDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<ScreenHeader, ScreenHeaderForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,Screens.List,Screens.All", Priority = 1)]
        public async Task<List<ScreenHeaderForReturnDto>> GetListAsync()
        {
            var numberOfRoomsList = await screenHeaderDal.GetListAsync();
            if (numberOfRoomsList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<ScreenHeader>, List<ScreenHeaderForReturnDto>>(numberOfRoomsList);
            return mapForReturn;
        }

       [SecuredOperation("Sudo,Screens.Update,Screens.All", Priority = 1)]
        [ValidationAspect(typeof(ScreenHeaderValidator), Priority = 2)]
        public async Task<ScreenHeaderForReturnDto> Update(ScreenHeaderForCreationDto updateDto)
        {
             var checkFromDb=await screenHeaderDal.GetAsync(x=>x.Id==updateDto.Id);
             if(checkFromDb==null)
             {
                  throw new RestException(HttpStatusCode.BadRequest,new{NotFound=Messages.NotFound});
             }

             var mapForUpdate=mapper.Map(updateDto,checkFromDb);
             await screenHeaderDal.Update(mapForUpdate);
             var mapForReturn=mapper.Map<ScreenHeader,ScreenHeaderForReturnDto>(mapForUpdate);
             return mapForReturn;

        }
    }
}
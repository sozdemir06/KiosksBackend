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
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class NumberOfRoomManager : INumberOfRoomService
    {
        private readonly INumberOfRoomDal numberOfRoomDal;
        private readonly IMapper mapper;
        public NumberOfRoomManager(INumberOfRoomDal numberOfRoomDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.numberOfRoomDal = numberOfRoomDal;

        }

        [SecuredOperation("Sudo,HomeAnnounceOptions.All",Priority=1)]        
        [ValidationAspect(typeof(NumberOfRoomValidator),Priority=2)]
        public async Task<NumberOfRoomForReturnDto> Create(NumberOfRoomForCreateOrUpdateDto createDto)
        {
             var numberOfRoomsName=await numberOfRoomDal.GetAsync(x=>x.Name.ToLower()==createDto.Name.ToLower());
             if(numberOfRoomsName!=null)
             {
                 throw new RestException(HttpStatusCode.BadRequest,new{AlreadyExist=Messages.AlreadyExist});
             }

            var mapForCreate=mapper.Map<NumberOfRoom>(createDto);
            var createToDb=await numberOfRoomDal.Add(mapForCreate);
            var mapForReturn=mapper.Map<NumberOfRoom,NumberOfRoomForReturnDto>(createToDb);
            return mapForReturn;

        }

         [SecuredOperation("Sudo,HomeAnnounceOptions.All",Priority=1)]   
        public async Task<NumberOfRoomForReturnDto> Delete(int Id)
        {
             var checkFromDb=await numberOfRoomDal.GetAsync(x=>x.Id==Id);
             if(checkFromDb==null)
             {
                  throw new RestException(HttpStatusCode.BadRequest,new{NotFound=Messages.NotFound});
             }

             await numberOfRoomDal.Delete(checkFromDb);
             var mapForReturn=mapper.Map<NumberOfRoom,NumberOfRoomForReturnDto>(checkFromDb);
             return mapForReturn;
        }


         [SecuredOperation("Sudo,HomeAnnounceOptions.All",Priority=1)]   
        public async Task<List<NumberOfRoomForReturnDto>> GetListAsync()
        {
             var numberOfRoomsList=await numberOfRoomDal.GetListAsync();
             if(numberOfRoomsList==null)
             {
                 throw new RestException(HttpStatusCode.BadRequest,new{NotFound=Messages.NotFound});
             }

             var mapForReturn=mapper.Map<List<NumberOfRoom>,List<NumberOfRoomForReturnDto>>(numberOfRoomsList);
             return mapForReturn;
        }

      
        [SecuredOperation("Sudo,HomeAnnounceOptions.All",Priority=1)]   
        [ValidationAspect(typeof(NumberOfRoomValidator),Priority=2)]
        public async Task<NumberOfRoomForReturnDto> Update(NumberOfRoomForCreateOrUpdateDto updateDto)
        {
             var checkFromDb=await numberOfRoomDal.GetAsync(x=>x.Id==updateDto.Id);
             if(checkFromDb==null)
             {
                  throw new RestException(HttpStatusCode.BadRequest,new{NotFound=Messages.NotFound});
             }

             var mapForUpdate=mapper.Map(updateDto,checkFromDb);
             await numberOfRoomDal.Update(mapForUpdate);
             var mapForReturn=mapper.Map<NumberOfRoom,NumberOfRoomForReturnDto>(mapForUpdate);
             return mapForReturn;

        }
    }
}
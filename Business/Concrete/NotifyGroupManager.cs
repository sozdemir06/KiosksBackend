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
    public class NotifyGroupManager : INotifyGroupService
    {
        private readonly INotifyGroupDal notifyGroupDal;
        private readonly IMapper mapper;
        public NotifyGroupManager(INotifyGroupDal notifyGroupDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.notifyGroupDal = notifyGroupDal;

        }
        [SecuredOperation("Sudo,User.All", Priority = 1)]
        [ValidationAspect(typeof(NotifyGroupValidation), Priority = 2)]
        public async Task<NotifyGroupForReturnDto> Create(NotifyGroupForCreationDto createDto)
        {
            var checkByName = await notifyGroupDal.GetAsync(x => x.GroupName.ToLower() == createDto.GroupName.ToLower());
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<NotifyGroup>(createDto);
            var saveToDb = await notifyGroupDal.Add(mapForCreate);
            var mapForReturn = mapper.Map<NotifyGroup, NotifyGroupForReturnDto>(saveToDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo", Priority = 1)]
        public async Task<NotifyGroupForReturnDto> Delete(int Id)
        {
            var checkFromDb = await notifyGroupDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await notifyGroupDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<NotifyGroup, NotifyGroupForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,User.All", Priority = 1)]
        public async Task<List<NotifyGroupForReturnDto>> GetListAsync()
        {
            var buildingsAgeList = await notifyGroupDal.GetListAsync();
            if (buildingsAgeList == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<NotifyGroup>, List<NotifyGroupForReturnDto>>(buildingsAgeList);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,User.All", Priority = 1)]
        [ValidationAspect(typeof(NotifyGroupValidation), Priority = 2)]
        public async Task<NotifyGroupForReturnDto> Update(NotifyGroupForCreationDto updateDto)
        {
            var checkById = await notifyGroupDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await notifyGroupDal.Update(mapForUpdate);
            return mapper.Map<NotifyGroup, NotifyGroupForReturnDto>(mapForUpdate);
        }
    }
}
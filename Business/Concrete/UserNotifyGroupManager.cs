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
using DataAccess.EntitySpecification.USerNotifyGroupsSpecification;
using Entities.Dtos;

namespace Business.Concrete
{
    public class UserNotifyGroupManager : IUserNotifyGroupService
    {
        private readonly IUserNotifyGroupDal userNotifyGroupDal;
        private readonly IMapper mapper;
        private readonly IUserDal userDal;
        private readonly INotifyGroupDal notifyGroupDal;
        public UserNotifyGroupManager(IUserNotifyGroupDal userNotifyGroupDal,
         INotifyGroupDal notifyGroupDal,
        IMapper mapper, IUserDal userDal)
        {
            this.notifyGroupDal = notifyGroupDal;
            this.userDal = userDal;
            this.mapper = mapper;
            this.userNotifyGroupDal = userNotifyGroupDal;

        }

        [SecuredOperation("Sudo,User.All", Priority = 1)]
        [ValidationAspect(typeof(UserNotifyGroupValidator), Priority = 2)]
        public async Task<UserNotifyGroupForReturnDto> Create(UserNotifyGroupForCreationDto createDto)
        {
            var userFromRepo = await userDal.GetAsync(x => x.Id == createDto.UserId);
            if (userFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = "Kullanıcı bulunamadı.." });
            }

            var notifyGroupFromRepo = await notifyGroupDal.GetAsync(x => x.Id == createDto.NotifyGroupId);
            if (notifyGroupFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = "Bildirim Gurubu bulunamadı..." });
            }
            var checkByName = await userNotifyGroupDal.GetAsync(x => x.UserId == createDto.UserId && x.NotifyGroupId == createDto.NotifyGroupId);
            if (checkByName != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.AlreadyExist });
            }

            var mapForCreate = mapper.Map<UserNotifyGroup>(createDto);
            var saveToDb = await userNotifyGroupDal.Add(mapForCreate);
            var spec=new UserNotifyGroupWithByNotifyGroupId(saveToDb.NotifyGroupId);
            var getUserNotifyGroup=await userNotifyGroupDal.GetEntityWithSpecAsync(spec);
            var mapForReturn = mapper.Map<UserNotifyGroup, UserNotifyGroupForReturnDto>(getUserNotifyGroup);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,User.All", Priority = 1)]
        public async Task<UserNotifyGroupForReturnDto> Delete(int Id)
        {
            var checkFromDb = await userNotifyGroupDal.GetAsync(x => x.Id == Id);
            if (checkFromDb == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            await userNotifyGroupDal.Delete(checkFromDb);
            var mapForReturn = mapper.Map<UserNotifyGroup, UserNotifyGroupForReturnDto>(checkFromDb);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,User.All", Priority = 1)]
        public async Task<List<UserNotifyGroupForReturnDto>> GetListAsync()
        {
            var spec=new UserNotifyGroupWithNotifyGroupSpecification();
            var getUserNotifyGroup=await userNotifyGroupDal.ListEntityWithSpecAsync(spec);
            if (getUserNotifyGroup == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForReturn = mapper.Map<List<UserNotifyGroup>, List<UserNotifyGroupForReturnDto>>(getUserNotifyGroup);
            return mapForReturn;
        }

        [SecuredOperation("Sudo,User.All", Priority = 1)]
        public async Task<List<UserNotifyGroupForReturnDto>> GetListByUserId(int userId)
        {
            var userFromRepo = await userDal.GetAsync(x => x.Id == userId);
            if (userFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = "Kullanıcı bulunamadı.." });
            }

             var spec=new UserNotifyGroupWithNotifyGroupSpecification(userId);
            var getUserNotifyGroup=await userNotifyGroupDal.ListEntityWithSpecAsync(spec);
            
            return mapper.Map<List<UserNotifyGroup>,List<UserNotifyGroupForReturnDto>> (getUserNotifyGroup);

        }

        [SecuredOperation("Sudo,User.All", Priority = 1)]
        [ValidationAspect(typeof(UserNotifyGroupValidator), Priority = 2)]
        public async Task<UserNotifyGroupForReturnDto> Update(UserNotifyGroupForCreationDto updateDto)
        {
            var userFromRepo = await userDal.GetAsync(x => x.Id == updateDto.UserId);
            if (userFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = "Kullanıcı bulunamadı.." });
            }

            var notifyGroupFromRepo = await notifyGroupDal.GetAsync(x => x.Id == updateDto.Id);
            if (notifyGroupFromRepo == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { UserNotFound = "Bildirim Gurubu bulunamadı..." });
            }

            var checkById = await userNotifyGroupDal.GetAsync(x => x.Id == updateDto.Id);
            if (checkById == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { NotFound = Messages.NotFound });
            }

            var mapForUpdate = mapper.Map(updateDto, checkById);
            await userNotifyGroupDal.Update(mapForUpdate);

             var spec=new UserNotifyGroupWithNotifyGroupSpecification();
            var getUserNotifyGroup=await userNotifyGroupDal.GetEntityWithSpecAsync(spec);
            
            return mapper.Map<UserNotifyGroup, UserNotifyGroupForReturnDto>(getUserNotifyGroup);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Security.UserAccessor;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class OnlineUserManager : IOnlineUserService
    {
        private readonly IOnlineUserDal onlineUserDal;
        private readonly IMapper mapper;
        private readonly IUserAccessor userAccessor;
        public OnlineUserManager(IOnlineUserDal onlineUserDal, IMapper mapper, IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;
            this.mapper = mapper;
            this.onlineUserDal = onlineUserDal;

        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        [ValidationAspect(typeof(OnlineUserValidator), Priority = 2)]
        public async Task<OnlineUserForReturnDto> AddNewOnlineUserAsync(OnlineUser onlineUser)
        {
            var checkIfExist = await onlineUserDal.GetAsync(x => x.UserId == onlineUser.UserId);
            if (checkIfExist != null)
            {
                await onlineUserDal.Delete(checkIfExist);
            }

            await onlineUserDal.Add(onlineUser);
            return mapper.Map<OnlineUser, OnlineUserForReturnDto>(onlineUser);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<List<OnlineUserForReturnDto>> GetAllOnlineUserAsync()
        {
            var allOnlineUser = await onlineUserDal.GetListAsync();
            return mapper.Map<List<OnlineUser>, List<OnlineUserForReturnDto>>(allOnlineUser);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<OnlineUserForReturnDto> GetOnlineUserByIdAsync(int userId)
        {
            var onlineuser = await onlineUserDal.GetAsync(x => x.UserId == userId);
            return mapper.Map<OnlineUser, OnlineUserForReturnDto>(onlineuser);
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task<string> GetUserConnectionStringAsync()
        {
            var userId=await userAccessor.GetUserClaimId();
            var onlineUser=await onlineUserDal.GetAsync(x=>x.UserId==userId);
            return onlineUser.ConnectionId;
        }

        [SecuredOperation("Sudo,Public", Priority = 1)]
        public async Task RemoveByUserIdAsync(int userId)
        {
            var checkIfExist = await onlineUserDal.GetAsync(x => x.UserId == userId);
            if (checkIfExist != null)
            {
                await onlineUserDal.Delete(checkIfExist);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.ValidaitonRules.FluentValidation;
using Core.Aspects.AutoFac.Validation;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class OnlineScreenManager : IOnlineScreenService
    {
        private readonly IMapper mapper;
        private readonly IOnlineScreenDal onlineScreenDal;
        private readonly IScreenDal screenDal;
        public OnlineScreenManager(IOnlineScreenDal onlineScreenDal, IMapper mapper, IScreenDal screenDal)
        {
            this.screenDal = screenDal;
            this.onlineScreenDal = onlineScreenDal;
            this.mapper = mapper;

        }


        [ValidationAspect(typeof(OnlineScreenValidator), Priority = 2)]
        public async Task<OnlineScreenForReturnDto> AddNewOnlineScreenAsync(OnlineScreen onlineScreen)
        {
            var checkScreen = await screenDal.GetAsync(x => x.Id == onlineScreen.ScreenId);
            if (checkScreen != null)
            {

                await onlineScreenDal.Add(onlineScreen);
            }

            return mapper.Map<OnlineScreen, OnlineScreenForReturnDto>(onlineScreen);
        }


        public async Task<List<OnlineScreenForReturnDto>> GetAllOnlineScreenAsync()
        {
            var allOnlineScreen = await onlineScreenDal.GetListAsync();
            return mapper.Map<List<OnlineScreen>, List<OnlineScreenForReturnDto>>(allOnlineScreen);
        }

        public async Task<string[]> GetAllOnlineScreenConnectionId()
        {
            var getAllOnlineScreens = await onlineScreenDal.GetListAsync();
            string[] connections = null;
            if (getAllOnlineScreens.Count > 0)
            {
                connections = getAllOnlineScreens.Select(x => x.ConnectionId).ToArray();
            }
            return connections;
        }

        public async Task<OnlineScreenForReturnDto> GetOnlineScreenByIdAsync(int screenId)
        {
            var onlineScreen = await onlineScreenDal.GetAsync(x => x.ScreenId == screenId);
            return mapper.Map<OnlineScreen, OnlineScreenForReturnDto>(onlineScreen);
        }

        public async Task<string[]> GetOnlineScreenConnectionIdByScreenId(int screenId)
        {
            var getAllOnlineScreens = await onlineScreenDal.GetListAsync(x=>x.ScreenId==screenId);
            string[] connections = null;
            if (getAllOnlineScreens.Count > 0)
            {
                connections = getAllOnlineScreens.Select(x => x.ConnectionId).ToArray();
            }
            return connections;
        }

        public async Task<string> GetScreenByConnectionIdAsync(string connectionId)
        {
            var onlineScreen = await onlineScreenDal.GetAsync(x => x.ConnectionId == connectionId);
            if (onlineScreen != null)
            {
                return onlineScreen.ConnectionId;
            }

            return null;

        }

        public async Task<string> GetScreenConnectionStringAsync(int screenId)
        {
            var onlineScreen = await onlineScreenDal.GetAsync(x => x.ScreenId == screenId);
            if (onlineScreen != null)
            {
                return onlineScreen.ConnectionId;
            }

            return null;
        }

        public async Task RemoveByConnectionIdAsync(string connectionId)
        {
            var checkIfExist = await onlineScreenDal.GetAsync(x => x.ConnectionId.ToLower() == connectionId.ToLower());
            if (checkIfExist != null)
            {
                await onlineScreenDal.Delete(checkIfExist);
            }
        }

        public async Task RemoveByScreenIdAsync(int screenId)
        {
            var checkIfExist = await onlineScreenDal.GetAsync(x => x.ScreenId == screenId);
            if (checkIfExist != null)
            {
                await onlineScreenDal.Delete(checkIfExist);
            }
        }
    }
}
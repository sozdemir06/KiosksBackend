using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class KiosksHub : Hub
    {
        private readonly IOnlineScreenService onlineScreenService;

        public KiosksHub(IOnlineScreenService onlineScreenService)
        {
            this.onlineScreenService = onlineScreenService;


        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("OnConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var screenConnectionId = await onlineScreenService.GetScreenByConnectionIdAsync(Context.ConnectionId);
            if (screenConnectionId != null)
            {
                await onlineScreenService.RemoveByConnectionIdAsync(screenConnectionId);
                var onlineScreens = await onlineScreenService.GetAllOnlineScreenAsync();
                var connectedScreenId = onlineScreens.Select(x => x.ScreenId).ToArray();
                await Clients.All.SendAsync("OnlineScreens", connectedScreenId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task ConnectScreen(int screenId, string connectionId)
        {

            if (screenId > 0)
            {
                var onlineScreen = new OnlineScreen
                {
                    ScreenId = screenId,
                    ConnectionId = connectionId
                };
                var addNewScreen = await onlineScreenService.AddNewOnlineScreenAsync(onlineScreen);
                var onlineScreens = await onlineScreenService.GetAllOnlineScreenAsync();
                var connectedScreenId = onlineScreens.Select(x => x.ScreenId).ToArray();
                await Clients.All.SendAsync("OnlineScreens", connectedScreenId);
            }



        }


    }
}
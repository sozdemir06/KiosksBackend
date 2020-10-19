using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Security.UserAccessor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    [Authorize]
    public class AdminHub : Hub
    {
        private readonly IAnnounceService announceService;
        private readonly IUserAccessor userAccessor;
        private readonly INewsService newsService;
        private readonly IOnlineUserService onlineUserService;
        private readonly IUserNotifyGroupService userNotifyGroupService;
        private readonly IHomeAnnounceService homeAnnounce;
        private readonly IVehicleAnnounceService vehicleAnnounce;
        public AdminHub(IAnnounceService announceService,
        IUserAccessor userAccessor,INewsService newsService,IOnlineUserService onlineUserService, 
        IUserNotifyGroupService userNotifyGroupService,
        IHomeAnnounceService homeAnnounce,
        IVehicleAnnounceService vehicleAnnounce)
        {
            this.vehicleAnnounce = vehicleAnnounce;
            this.homeAnnounce = homeAnnounce;
            this.announceService = announceService;
            this.userAccessor = userAccessor;
            this.newsService = newsService;
            this.onlineUserService = onlineUserService;
            this.userNotifyGroupService = userNotifyGroupService;
   
        }

        public override async Task OnConnectedAsync()
        {
           
            var userId=await userAccessor.GetUserClaimId();
            var userGroups = await userNotifyGroupService.GetListByUserId(userId);
            
            if (userGroups.Count > 0)
            {
                foreach (var group in userGroups)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, group.GroupName);
                }
            }
            await onlineUserService.AddNewOnlineUserAsync(new OnlineUser{UserId=userId,ConnectionId=Context.ConnectionId});
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            var userId=await userAccessor.GetUserClaimId();
            var userGroups = await userNotifyGroupService.GetListByUserId(userId);
            if (userGroups.Count > 0)
            {
                foreach (var group in userGroups)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.GroupName);
                }
            }
            await onlineUserService.RemoveByUserIdAsync(userId);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
using System.Threading.Tasks;
using Business.Abstract;
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
        private readonly IUserNotifyGroupService userNotifyGroupService;
        private readonly IHomeAnnounceService homeAnnounce;
        private readonly IVehicleAnnounceService vehicleAnnounce;
        public AdminHub(IAnnounceService announceService,IUserAccessor userAccessor, 
        IUserNotifyGroupService userNotifyGroupService,
        IHomeAnnounceService homeAnnounce,
        IVehicleAnnounceService vehicleAnnounce)
        {
            this.vehicleAnnounce = vehicleAnnounce;
            this.homeAnnounce = homeAnnounce;
            this.announceService = announceService;
            this.userAccessor = userAccessor;
            this.userNotifyGroupService = userNotifyGroupService;
   
        }

        public override async Task OnConnectedAsync()
        {
           
           
            var userGroups = await userNotifyGroupService.GetListByUserId(await userAccessor.GetUserClaimId());
            if (userGroups.Count > 0)
            {
                foreach (var group in userGroups)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, group.GroupName);
                }
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
           
            var userGroups = await userNotifyGroupService.GetListByUserId(await userAccessor.GetUserClaimId());
            if (userGroups.Count > 0)
            {
                foreach (var group in userGroups)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.GroupName);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
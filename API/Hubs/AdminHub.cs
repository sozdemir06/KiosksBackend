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
        private readonly IUserAccessor userAccessor;
        private readonly IUserNotifyGroupService userNotifyGroupService;
        private readonly UserTracker userTracker;
        public AdminHub(IUserAccessor userAccessor,
        UserTracker userTracker,
        IUserNotifyGroupService userNotifyGroupService)
        {
            this.userTracker = userTracker;
            this.userAccessor = userAccessor;
            this.userNotifyGroupService = userNotifyGroupService;

        }

        public override async Task OnConnectedAsync()
        {

            var userId = await userAccessor.GetUserClaimId();
            var userGroups = await userNotifyGroupService.GetListByUserId(userId);

            if (userGroups.Count > 0)
            {
                foreach (var group in userGroups)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, group.GroupName);
                }
            }
            await userTracker.UserConnected(userId,Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            var userId = await userAccessor.GetUserClaimId();
            var userGroups = await userNotifyGroupService.GetListByUserId(userId);
            if (userGroups.Count > 0)
            {
                foreach (var group in userGroups)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.GroupName);
                }
            }
            await userTracker.UserDisConnected(userId,Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
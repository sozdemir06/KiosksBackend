using System.Threading.Tasks;
using API.Hubs;
using Business.Abstract;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAnnouncesController : ControllerBase
    {
        private readonly IHomeAnnounceService homeAnnounceService;
        private readonly UserTracker userTracker;
        private readonly IKiosksService kiosksService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<AdminHub> hubContext;
        public HomeAnnouncesController(IHomeAnnounceService homeAnnounceService, UserTracker userTracker,
        IKiosksService kiosksService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService,
         IHubContext<AdminHub> hubContext)
        {
            this.hubContext = hubContext;
            this.homeAnnounceService = homeAnnounceService;
            this.userTracker = userTracker;
            this.kiosksService = kiosksService;
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<HomeAnnounceForReturnDto>>> List([FromQuery] HomeAnnounceParams queryParams)
        {
            return await homeAnnounceService.GetListAsync(queryParams);
        }

        [HttpPost]
        public async Task<ActionResult<HomeAnnounceForReturnDto>> Create([FromBody] HomeAnnounceForCreationDto creationDto)
        {
            var homeAnnounce = await homeAnnounceService.Create(creationDto);
            var connIds = await userTracker.GetOnlineUser();
            if (connIds != null && connIds.Length != 0)
            {
                await hubContext.Clients.GroupExcept("Home", connIds).SendAsync("ReceiveNewHomeAnnounce", homeAnnounce, true);
            }

            return homeAnnounce;
        }

        [HttpPut]
        public async Task<ActionResult<HomeAnnounceForReturnDto>> Update(HomeAnnounceForCreationDto updateDto)
        {
            var homeAnnounce = await homeAnnounceService.Update(updateDto);
            var connIds = await userTracker.GetOnlineUser();
            if (connIds != null && connIds.Length != 0)
            {
                await hubContext.Clients.GroupExcept("Home", connIds).SendAsync("ReceiveUpdateHomeAnnounce", homeAnnounce);
            }

            var onlineScreensConnectionId = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreensConnectionId != null && onlineScreensConnectionId.Length != 0)
            {

                await kiosksHub.Clients.Clients(onlineScreensConnectionId).SendAsync("ReloadScreen", true);

            }

            return homeAnnounce;
        }

        [HttpPut("publish")]
        public async Task<ActionResult<HomeAnnounceForReturnDto>> Publish(HomeAnnounceForCreationDto updateDto)
        {
            var homeAnnounce = await homeAnnounceService.Publish(updateDto);
            var onlineScreensConnectionId = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreensConnectionId != null && onlineScreensConnectionId.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreensConnectionId).SendAsync("ReloadScreen", true);

            }

            return homeAnnounce;
        }

        [HttpDelete("{announceId}")]
        public async Task<ActionResult<HomeAnnounceForReturnDto>> Delete(int announceId)
        {
            return await homeAnnounceService.Delete(announceId);
        }

        [HttpPost("createforuser/{userId}")]
        public async Task<ActionResult<HomeAnnounceForUserDto>> CreateForUser([FromBody] HomeAnnounceForCreationDto creationDto, int userId)
        {
            var homeAnnounce = await homeAnnounceService.CreateForPublicAsync(creationDto, userId);
            var connIds = await userTracker.GetOnlineUser();
            if (connIds != null && connIds.Length != 0)
            {
                await hubContext.Clients.GroupExcept("Home", connIds).SendAsync("ReceiveNewHomeAnnounce", homeAnnounce, true);
            }

            return homeAnnounce;
        }

        [HttpPut("updateforuser/{userId}")]
        public async Task<ActionResult<HomeAnnounceForUserDto>> UpdateForPublic(HomeAnnounceForCreationDto updateDto, int userId)
        {
            var homeAnnounce = await homeAnnounceService.UpdateForPublicAsync(updateDto, userId);
            var connIds = await userTracker.GetOnlineUser();
            if (connIds != null && connIds.Length != 0)
            {
                await hubContext.Clients.GroupExcept("Home", connIds).SendAsync("ReceiveUpdateHomeAnnounce", homeAnnounce, true);
            }

            return homeAnnounce;
        }
    }
}
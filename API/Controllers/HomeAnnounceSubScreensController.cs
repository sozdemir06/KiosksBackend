using System.Collections.Generic;
using System.Threading.Tasks;
using API.Hubs;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAnnounceSubScreensController : ControllerBase
    {
        private readonly IHomeAnnounceSubScreenService homeAnnounceSubScreenService;
        private readonly IKiosksService kiosksService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        public HomeAnnounceSubScreensController(IHomeAnnounceSubScreenService homeAnnounceSubScreenService,
        IKiosksService kiosksService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
            this.homeAnnounceSubScreenService = homeAnnounceSubScreenService;
            this.kiosksService = kiosksService;
        }

        [HttpPost]
        public async Task<ActionResult<HomeAnnounceSubScreenForReturnDto>> Create(HomeAnnounceSubScreenForCreationDto creationDto)
        {
            var subscreen = await homeAnnounceSubScreenService.Create(creationDto);
            var onlineScreensConnectionId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensConnectionId != null && onlineScreensConnectionId.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreensConnectionId).SendAsync("ReloadScreen", true);

            }
            return subscreen;
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<HomeAnnounceSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await homeAnnounceSubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<HomeAnnounceSubScreenForReturnDto>> Delete(int Id)
        {
            var subscreen = await homeAnnounceSubScreenService.Delete(Id);
            var onlineScreensConnectionId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensConnectionId != null && onlineScreensConnectionId.Length != 0)
            {
                 await kiosksHub.Clients.Clients(onlineScreensConnectionId).SendAsync("ReloadScreen", true);
            }
            return subscreen;
        }
    }
}
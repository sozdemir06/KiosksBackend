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
    public class AnnounceSubScreensController : ControllerBase
    {
        private readonly IAnnounceSubScreenService announceSubScreenService;
        private readonly IKiosksService kiosksService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        public AnnounceSubScreensController(IAnnounceSubScreenService announceSubScreenService,
        IKiosksService kiosksService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
            this.announceSubScreenService = announceSubScreenService;
            this.kiosksService = kiosksService;
        }

        [HttpPost]
        public async Task<ActionResult<AnnounceSubScreenForReturnDto>> Create(AnnounceSubScreenForCreationDto creationDto)
        {
            var subscreen = await announceSubScreenService.Create(creationDto);
            var onlineScreensById = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensById != null && onlineScreensById.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreensById).SendAsync("ReloadScreen", true);
            }

            return subscreen;
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<AnnounceSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await announceSubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<AnnounceSubScreenForReturnDto>> Delete(int Id)
        {
            var subscreen = await announceSubScreenService.Delete(Id);
            var onlineScreensById = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensById != null && onlineScreensById.Length != 0)
            {

                await kiosksHub.Clients.Clients(onlineScreensById).SendAsync("ReloadScreen",true);

            }

            return subscreen;
        }
    }
}
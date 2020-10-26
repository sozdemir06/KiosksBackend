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
    public class NewsSubScreenController : ControllerBase
    {
        private readonly INewsSubScreenService newsSubScreenService;
        private readonly IKiosksService kiosksService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        public NewsSubScreenController(INewsSubScreenService newsSubScreenService,
        IKiosksService kiosksService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.onlineScreenService = onlineScreenService;
            this.newsSubScreenService = newsSubScreenService;
            this.kiosksService = kiosksService;
            this.kiosksHub = kiosksHub;
        }

        [HttpPost]
        public async Task<ActionResult<NewsSubScreenForReturnDto>> Create(NewsSubScreenForCreationDto creationDto)
        {
            var subscreen = await newsSubScreenService.Create(creationDto);
            var onlineScreensConnectionId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensConnectionId != null && onlineScreensConnectionId.Length != 0)
            {
                var newsForKiosks=await kiosksService.GetNewsById(subscreen.NewsId);
                if(newsForKiosks!=null)
                {
                    await kiosksHub.Clients.Clients(onlineScreensConnectionId).SendAsync("ReceiveNewsSubScreen", subscreen, "create",newsForKiosks);
                }
               
            }

            return subscreen;
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<NewsSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await newsSubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<NewsSubScreenForReturnDto>> Delete(int Id)
        {
            var subscreen = await newsSubScreenService.Delete(Id);
            var onlineScreensConnectionId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensConnectionId != null && onlineScreensConnectionId.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreensConnectionId).SendAsync("ReceiveNewsSubScreen", subscreen, "delete",null);
            }

            return subscreen;
        }
    }
}
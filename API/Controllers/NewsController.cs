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
    public class NewsController : ControllerBase
    {
        private readonly INewsService newsService;
        private readonly UserTracker userTracker;
        private readonly IHubContext<AdminHub> hubContext;
        private readonly IKiosksService kiosksService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        public NewsController(INewsService newsService, UserTracker userTracker,
        IHubContext<AdminHub> hubContext,
        IKiosksService kiosksService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.onlineScreenService = onlineScreenService;
            this.hubContext = hubContext;
            this.kiosksService = kiosksService;
            this.kiosksHub = kiosksHub;
            this.newsService = newsService;
            this.userTracker = userTracker;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<NewsForReturnDto>>> List([FromQuery] NewsParams queryParams)
        {
            return await newsService.GetListAsync(queryParams);
        }

        [HttpPost()]
        public async Task<ActionResult<NewsForReturnDto>> Create([FromBody] NewsForCreationDto creationDto)
        {
            var news = await newsService.Create(creationDto);
            var connIds = await userTracker.GetOnlineUser();
            if (connIds != null && connIds.Length != 0)
            {
                await hubContext.Clients.GroupExcept("News", connIds).SendAsync("ReceiveNewNews", news, true);
            }

            return news;

        }

        [HttpPut()]
        public async Task<ActionResult<NewsForReturnDto>> Update(NewsForCreationDto updateDto)
        {
            var news = await newsService.Update(updateDto);
            var connIds = await userTracker.GetOnlineUser();
            if (connIds != null && connIds.Length != 0)
            {
                await hubContext.Clients.GroupExcept("News", connIds).SendAsync("ReceiveUpdateNews", news);
            }

            var screenConnectionId = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (screenConnectionId != null && screenConnectionId.Length != 0)
            {

                await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReloadScreen", true);

            }

            return news;
        }

        [HttpPut("publish")]
        public async Task<ActionResult<NewsForReturnDto>> Publish(NewsForCreationDto updateDto)
        {
            var news = await newsService.Publish(updateDto);
            var screenConnectionId = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (screenConnectionId != null && screenConnectionId.Length != 0)
            {
              await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReloadScreen",true);
            }

            return news;
        }

        [HttpDelete("{announceId}")]
        public async Task<ActionResult<NewsForReturnDto>> Delete(int announceId)
        {
            return await newsService.Delete(announceId);
        }
    }
}
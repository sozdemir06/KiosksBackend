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
        private readonly IHubContext<AdminHub> hubContext;
        private readonly IKiosksService kiosksService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineUserService onlineUserService;
        private readonly IOnlineScreenService onlineScreenService;
        public NewsController(INewsService newsService, IHubContext<AdminHub> hubContext, IKiosksService kiosksService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService,
            IOnlineUserService onlineUserService)
        {
            this.onlineScreenService = onlineScreenService;
            this.onlineUserService = onlineUserService;
            this.hubContext = hubContext;
            this.kiosksService = kiosksService;
            this.kiosksHub = kiosksHub;
            this.newsService = newsService;

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
            var connectionId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connectionId))
            {
                await hubContext.Clients.GroupExcept("News", connectionId).SendAsync("ReceiveNewNews", news);
            }

            return news;

        }

        [HttpPut()]
        public async Task<ActionResult<NewsForReturnDto>> Update(NewsForCreationDto updateDto)
        {
            var news = await newsService.Update(updateDto);
            var connectionId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connectionId))
            {
                await hubContext.Clients.GroupExcept("News", connectionId).SendAsync("ReceiveUpdateNews", news);
            }

            var screenConnectionId = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (screenConnectionId != null && screenConnectionId.Length != 0)
            {
                var newsForKiosks = await kiosksService.GetNewsById(news.Id);
                if (newsForKiosks != null)
                {
                    await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReceiveNews", newsForKiosks);
                }
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
                var newForKiosks = await kiosksService.GetNewsById(news.Id);
                if (newForKiosks != null)
                {
                    await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReceiveNews", news);
                }

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
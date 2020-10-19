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
    public class NewsPhotoController : ControllerBase
    {
        private readonly INewsPhotoService newsPhotoService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<AdminHub> hubContext;
        private readonly IOnlineUserService onlineUserService;
        public NewsPhotoController(INewsPhotoService newsPhotoService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService,
        IOnlineUserService onlineUserService,
        IHubContext<AdminHub> hubContext)
        {
            this.onlineUserService = onlineUserService;
            this.hubContext = hubContext;
            this.newsPhotoService = newsPhotoService;
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<NewsPhotoForReturnDto>>> List(int announceId)
        {
            return await newsPhotoService.GetListAsync(announceId);
        }

        [HttpPost()]
        public async Task<ActionResult<NewsPhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            var photo = await newsPhotoService.Create(uploadDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("News", connId).SendAsync("ReceiveNewsPhoto", photo, "create");
            }

            return photo;
        }

        [HttpPut]
        public async Task<NewsPhotoForReturnDto> Update(NewsPhotoForCreationDto creationDto)
        {
            var photo = await newsPhotoService.Update(creationDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("News", connId).SendAsync("ReceiveNewsPhoto", photo, "update");
            }

            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveNewsPhoto", photo, "update");
            }

            return photo;
        }

        [HttpDelete("{photoId}")]
        public async Task<NewsPhotoForReturnDto> Delete(int photoId)
        {
            var photo = await newsPhotoService.Delete(photoId);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("News", connId).SendAsync("ReceiveNewsPhoto", photo, "delete");
            }

            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveNewsPhoto", photo, "delete");
            }

            return photo;
        }
    }
}
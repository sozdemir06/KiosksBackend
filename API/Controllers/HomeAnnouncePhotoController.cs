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
    public class HomeAnnouncePhotoController : ControllerBase
    {
        private readonly IHomeAnnouncePhotoService photoService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<AdminHub> hubContext;
        private readonly IOnlineUserService onlineUserService;
        public HomeAnnouncePhotoController(IHomeAnnouncePhotoService photoService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService,
         IOnlineUserService onlineUserService,
        IHubContext<AdminHub> hubContext)
        {
            this.onlineUserService = onlineUserService;
            this.hubContext = hubContext;
            this.photoService = photoService;
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
        }


        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<HomeAnnouncePhotoForReturnDto>>> List(int announceId)
        {
            return await photoService.GetListAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<HomeAnnouncePhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            var photo = await photoService.Create(uploadDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Home", connId).SendAsync("ReceiveNewHomeAnnouncePhoto", photo, "create");
            }

            return photo;
        }

        [HttpPut]
        public async Task<HomeAnnouncePhotoForReturnDto> Update(HomeAnnouncePhotoForCreationDto creationDto)
        {
            var photo = await photoService.Update(creationDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Home", connId).SendAsync("ReceiveNewHomeAnnouncePhoto", photo, "update");
            }
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null || onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveHomeAnnouncePhoto", photo, "update");
            }

            return photo;
        }

        [HttpDelete("{photoId}")]
        public async Task<HomeAnnouncePhotoForReturnDto> Delete(int photoId)
        {
            var photo = await photoService.Delete(photoId);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Home", connId).SendAsync("ReceiveNewHomeAnnouncePhoto", photo, "delete");
            }
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null || onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveHomeAnnouncePhoto", photo, "delete");
            }

            return photo;
        }

        [HttpPost("createforuser")]
        public async Task<ActionResult<HomeAnnouncePhotoForReturnDto>> CreateForPublic([FromForm] FileUploadDto uploadDto)
        {
            var photo = await photoService.CreateForPublicAsync(uploadDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Home", connId).SendAsync("ReceiveNewHomeAnnouncePhoto", photo, "create");
            }

            return photo;
        }
    }
}
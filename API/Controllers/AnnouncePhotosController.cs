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
    public class AnnouncePhotosController : ControllerBase
    {
        private readonly IAnnouncePhotoService announcePhotoService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IHubContext<AdminHub> hubContext;
        private readonly IOnlineUserService onlineUserService;
        public AnnouncePhotosController(IAnnouncePhotoService announcePhotoService,
        IOnlineScreenService onlineScreenService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineUserService onlineUserService,
        IHubContext<AdminHub> hubContext)
        {
            this.onlineUserService = onlineUserService;
            this.hubContext = hubContext;
            this.announcePhotoService = announcePhotoService;
            this.onlineScreenService = onlineScreenService;
            this.kiosksHub = kiosksHub;
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<AnnouncePhotoForReturnDto>>> List(int announceId)
        {
            return await announcePhotoService.GetListAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<AnnouncePhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            var photo = await announcePhotoService.Create(uploadDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if(!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Announce", connId).SendAsync("ReceiveNewPhotoAnnounce", photo, "create");
            }
            return photo;
        }

        [HttpPut]
        public async Task<AnnouncePhotoForReturnDto> Update(AnnouncePhotoForCretionDto creationDto)
        {
            var photo = await announcePhotoService.Update(creationDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Announce", connId).SendAsync("ReceiveNewPhotoAnnounce", photo, "update");
            }
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveAnnouncePhoto", photo, "update");
            }

            return photo;
        }

        [HttpDelete("{photoId}")]
        public async Task<AnnouncePhotoForReturnDto> Delete(int photoId)
        {
            var photo = await announcePhotoService.Delete(photoId);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Announce", connId).SendAsync("ReceiveNewPhotoAnnounce", photo, "delete");
            }
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveAnnouncePhoto", photo, "delete");
            }
            return photo;
        }

        [HttpPost("createforuser")]
        public async Task<ActionResult<AnnouncePhotoForReturnDto>> CreateForPublic([FromForm] FileUploadDto uploadDto)
        {
            var photo = await announcePhotoService.CreateForPublic(uploadDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if(!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Announce", connId).SendAsync("ReceiveNewPhotoAnnounce", photo, "create");
            }
          
            return photo;
        }
    }
}
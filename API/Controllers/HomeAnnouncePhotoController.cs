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
        private readonly UserTracker userTracker;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<AdminHub> hubContext;
        public HomeAnnouncePhotoController(IHomeAnnouncePhotoService photoService,
        UserTracker userTracker,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService,
        IHubContext<AdminHub> hubContext)
        {
            this.hubContext = hubContext;
            this.photoService = photoService;
            this.userTracker = userTracker;
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
             var connIds = await userTracker.GetOnlineUser();
            if (connIds!=null && connIds.Length!=0)
            {
                await hubContext.Clients.GroupExcept("Home", connIds).SendAsync("ReceiveNewHomeAnnouncePhoto", photo, "create",true);
            }

            return photo;
        }

        [HttpPut]
        public async Task<HomeAnnouncePhotoForReturnDto> Update(HomeAnnouncePhotoForCreationDto creationDto)
        {
            var photo = await photoService.Update(creationDto);
           var connIds = await userTracker.GetOnlineUser();
            if (connIds!=null && connIds.Length!=0)
            {
                await hubContext.Clients.GroupExcept("Home", connIds).SendAsync("ReceiveNewHomeAnnouncePhoto", photo, "update");
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
            var connIds = await userTracker.GetOnlineUser();
            if (connIds!=null && connIds.Length!=0)
            {
                await hubContext.Clients.GroupExcept("Home", connIds).SendAsync("ReceiveNewHomeAnnouncePhoto", photo, "delete");
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
            var connIds = await userTracker.GetOnlineUser();
            if (connIds!=null && connIds.Length!=0)
            {
                await hubContext.Clients.GroupExcept("Home", connIds).SendAsync("ReceiveNewHomeAnnouncePhoto", photo, "create",true);
            }

            return photo;
        }
    }
}
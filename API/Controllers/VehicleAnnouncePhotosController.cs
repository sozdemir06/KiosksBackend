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
    public class VehicleAnnouncePhotosController : ControllerBase
    {
        private readonly IVehicleAnnouncePhotoService vehicleAnnouncePhotoService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<AdminHub> hubContext;
        private readonly IOnlineUserService onlineUserService;
        public VehicleAnnouncePhotosController(IVehicleAnnouncePhotoService vehicleAnnouncePhotoService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService,
         IOnlineUserService onlineUserService,
        IHubContext<AdminHub> hubContext)
        {
            this.onlineUserService = onlineUserService;
            this.hubContext = hubContext;
            this.vehicleAnnouncePhotoService = vehicleAnnouncePhotoService;
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<VehicleAnnouncePhotoForReturnDto>>> List(int announceId)
        {
            return await vehicleAnnouncePhotoService.GetListAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleAnnouncePhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            var photo = await vehicleAnnouncePhotoService.Create(uploadDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Car", connId).SendAsync("ReceiveNewVehicleannouncePhoto", photo, "create");
            }

            return photo;
        }

        [HttpPost("createforuser")]
        public async Task<ActionResult<VehicleAnnouncePhotoForReturnDto>> CreateForUser([FromForm] FileUploadDto uploadDto)
        {
            var photo = await vehicleAnnouncePhotoService.CreateForPublicAsync(uploadDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Car", connId).SendAsync("ReceiveNewVehicleannouncePhoto", photo, "create");
            }

            return photo;
        }

        [HttpPut]
        public async Task<VehicleAnnouncePhotoForReturnDto> Update(VehicleAnnouncePhotoForCreationDto creationDto)
        {
            var photo = await vehicleAnnouncePhotoService.Update(creationDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Car", connId).SendAsync("ReceiveNewVehicleannouncePhoto", photo, "update");
            }

            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveVehicleAnnouncePhoto", photo, "update");
            }

            return photo;
        }

        [HttpDelete("{photoId}")]
        public async Task<VehicleAnnouncePhotoForReturnDto> Delete(int photoId)
        {
            var photo = await vehicleAnnouncePhotoService.Delete(photoId);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Car", connId).SendAsync("ReceiveNewVehicleannouncePhoto", photo, "delete");
            }

            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveVehicleAnnouncePhoto", photo, "delete");
            }

            return photo;
        }


    }
}
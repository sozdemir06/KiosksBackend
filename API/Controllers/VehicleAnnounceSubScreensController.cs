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
    public class VehicleAnnounceSubScreensController : ControllerBase
    {
        private readonly IVehicleAnnounceSubScreenService vehicleAnnouncesubScreenService;
        private readonly IKiosksService kiosksService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        public VehicleAnnounceSubScreensController(IVehicleAnnounceSubScreenService vehicleAnnouncesubScreenService,
        IKiosksService kiosksService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
            this.vehicleAnnouncesubScreenService = vehicleAnnouncesubScreenService;
            this.kiosksService = kiosksService;
        }

        [HttpPost]
        public async Task<ActionResult<VehicleAnnounceSubScreenForReturnDto>> Create(VehicleAnnounceSubScreenForCreationDto creationDto)
        {
            var subscreen = await vehicleAnnouncesubScreenService.Create(creationDto);
            var onlineScreensConnectionId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensConnectionId != null && onlineScreensConnectionId.Length != 0)
            {
                var vehicleAnnounceforKiosks = await kiosksService.GetVehicleAnnounceByIdAsync(subscreen.VehicleAnnounceId);
                if (vehicleAnnounceforKiosks != null)
                {
                    await kiosksHub.Clients.Clients(onlineScreensConnectionId).SendAsync("ReceiveVehicleAnnounceSubScreen", subscreen, "create",vehicleAnnounceforKiosks);
                }

            }

            return subscreen;
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<VehicleAnnounceSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await vehicleAnnouncesubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<VehicleAnnounceSubScreenForReturnDto>> Delete(int Id)
        {
            var subscreen = await vehicleAnnouncesubScreenService.Delete(Id);
            var onlineScreensConnectionId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensConnectionId != null && onlineScreensConnectionId.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreensConnectionId).SendAsync("ReceiveVehicleAnnounceSubScreen", subscreen, "delete",null);
            }

            return subscreen;
        }
    }
}
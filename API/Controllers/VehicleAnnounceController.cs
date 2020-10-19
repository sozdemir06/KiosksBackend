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
    public class VehicleAnnounceController : ControllerBase
    {
        private readonly IVehicleAnnounceService vehicleAnnounceService;
        private readonly IKiosksService kiosksService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<AdminHub> hubContext;
        private readonly IOnlineUserService onlineUserService;
        public VehicleAnnounceController(IVehicleAnnounceService vehicleAnnounceService,
        IKiosksService kiosksService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService,
        IOnlineUserService onlineUserService,
            IHubContext<AdminHub> hubContext)
        {
            this.onlineUserService = onlineUserService;
            this.hubContext = hubContext;
            this.vehicleAnnounceService = vehicleAnnounceService;
            this.kiosksService = kiosksService;
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
        }

        [HttpGet]
        public async Task<Pagination<VehicleAnnounceForReturnDto>> List([FromQuery] VehicleAnnounceParams queryParams)
        {
            return await vehicleAnnounceService.GetListAsync(queryParams);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleAnnounceForReturnDto>> Create(VehicleAnnounceForCreationDto creationDto)
        {
            var vehicleAnnounce = await vehicleAnnounceService.Create(creationDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Car", connId).SendAsync("ReceiveNewVehicleannounce", vehicleAnnounce);
            }

            return vehicleAnnounce;
        }

        [HttpPut]
        public async Task<ActionResult<VehicleAnnounceForReturnDto>> Update(VehicleAnnounceForCreationDto creationDto)
        {
            var vehicleAnnounce = await vehicleAnnounceService.Update(creationDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Car", connId).SendAsync("ReceiveUpdateVehicleannounce", vehicleAnnounce);
            }

            var screenConnectionId = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (screenConnectionId != null && screenConnectionId.Length != 0)
            {
                var vehicleAnnounceForKiosks = await kiosksService.GetVehicleAnnounceByIdAsync(vehicleAnnounce.Id);
                if (vehicleAnnounceForKiosks != null)
                {
                    await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReceiveVehicleAnnounce", vehicleAnnounceForKiosks);
                }
            }

            return vehicleAnnounce;
        }

        [HttpPut("publish")]
        public async Task<ActionResult<VehicleAnnounceForReturnDto>> Publish(VehicleAnnounceForCreationDto creationDto)
        {
            var vehicleAnnounce = await vehicleAnnounceService.Publish(creationDto);
            var screenConnectionId = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (screenConnectionId != null && screenConnectionId.Length != 0)
            {
                var vehicleAnnounceForKiosks = await kiosksService.GetVehicleAnnounceByIdAsync(vehicleAnnounce.Id);
                await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReceiveVehicleAnnounce", vehicleAnnounceForKiosks);

            }

            return vehicleAnnounce;
        }


        [HttpPost("createforuser/{userId}")]
        public async Task<ActionResult<VehicleAnnounceForUserDto>> CreateForPublic(VehicleAnnounceForCreationDto creationDto, int userId)
        {
            var vehicleAnnounce = await vehicleAnnounceService.CreateForPublicAsync(creationDto, userId);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Car", connId).SendAsync("ReceiveNewVehicleannounce", vehicleAnnounce);
            }

            return vehicleAnnounce;
        }
        [HttpPut("updateforuser/{userId}")]
        public async Task<ActionResult<VehicleAnnounceForUserDto>> UpdateForPublic(VehicleAnnounceForCreationDto creationDto, int userId)
        {
            var vehicleAnnounce = await vehicleAnnounceService.UpdateForPublicAsync(creationDto, userId);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Car", connId).SendAsync("ReceiveUpdateVehicleannounce", vehicleAnnounce);
            }

            return vehicleAnnounce;
        }
    }
}
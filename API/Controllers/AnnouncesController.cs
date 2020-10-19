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
    public class AnnouncesController : ControllerBase
    {
        private readonly IAnnounceService announceService;
        private readonly IKiosksService kiosksService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IAnnounceStatusCheck checkStatus;
        private readonly IHubContext<AdminHub> hubContext;
        private readonly IOnlineUserService onlineUserService;
        public AnnouncesController(IAnnounceService announceService,
        IKiosksService kiosksService,
        IOnlineScreenService onlineScreenService,
        IHubContext<KiosksHub> kiosksHub,
        IAnnounceStatusCheck checkStatus,
        IOnlineUserService onlineUserService,
            IHubContext<AdminHub> hubContext)
        {
            this.onlineUserService = onlineUserService;
            this.hubContext = hubContext;
            this.announceService = announceService;
            this.kiosksService = kiosksService;
            this.onlineScreenService = onlineScreenService;
            this.kiosksHub = kiosksHub;
            this.checkStatus = checkStatus;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<AnnounceForReturnDto>>> List([FromQuery] AnnounceParams queryParams)
        {
            return await announceService.GetListAsync(queryParams);
        }


        [HttpPost]
        public async Task<ActionResult<AnnounceForReturnDto>> Create([FromBody] AnnounceForCreationDto creationDto)
        {
            var announce = await announceService.Create(creationDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Announce", connId).SendAsync("ReceiveNewAnnounce", announce);
            }

            return announce;
        }

        [HttpPut]
        public async Task<ActionResult<AnnounceForReturnDto>> Update(AnnounceForCreationDto updateDto)
        {
            var announce = await announceService.Update(updateDto);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Announce", connId).SendAsync("ReceiveUpdateAnnounce", announce);
            }

            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                var announceForKiosks = await kiosksService.GetAnnounceByIdAsync(announce.Id);
                if (announceForKiosks != null)
                {
                    await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveAnnounce", announceForKiosks);
                }

            }

            return announce;
        }

        [HttpPut("publish")]
        public async Task<ActionResult<AnnounceForReturnDto>> Publish(AnnounceForCreationDto updateDto)
        {
            var announce = await announceService.Publish(updateDto);
            var screenConnectionId = await onlineScreenService.GetAllOnlineScreenConnectionId();

            if (screenConnectionId != null && screenConnectionId.Length != 0)
            {
                var announceForKiosks = await kiosksService.GetAnnounceByIdAsync(announce.Id);
                if (announceForKiosks != null)
                {
                    await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReceiveAnnounce", announceForKiosks);
                }

            }
            return announce;
        }

        [HttpDelete("{announceId}")]
        public async Task<ActionResult<AnnounceForReturnDto>> Delete(int announceId)
        {
            return await announceService.Delete(announceId);
        }

        [HttpPost("createforuser/{userId}")]
        public async Task<ActionResult<AnnounceForUserDto>> CreateForPublic([FromBody] AnnounceForCreationDto creationDto, int userId)
        {
            var announce = await announceService.CreateForPublicAsync(creationDto, userId);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Announce", connId).SendAsync("ReceiveNewAnnounce", announce);
            }
            return announce;
        }

        [HttpPut("updateforuser/{userId}")]
        public async Task<ActionResult<AnnounceForUserDto>> UpdateForPublic(AnnounceForCreationDto updateDto, int userId)
        {
            var announce = await announceService.UpdateForPublicAsync(updateDto, userId);
            var connId = await onlineUserService.GetUserConnectionStringAsync();
            if (!string.IsNullOrEmpty(connId))
            {
                await hubContext.Clients.GroupExcept("Announce", connId).SendAsync("ReceiveUpdateAnnounce", announce);
            }

            return announce;
        }



    }
}
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
    public class ScreenFootersController : ControllerBase
    {
        private readonly IScreenFooterService screenFooterService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;

        public ScreenFootersController(IScreenFooterService screenFooterService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.screenFooterService = screenFooterService;
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScreenFooterForReturnDto>>> List()
        {
            return await screenFooterService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ScreenFooterForReturnDto>> Create(ScreenFooterForCreationDto createDto)
        {
            var footer = await screenFooterService.Create(createDto);
            var screenConnectionId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(footer.ScreenId);
            if (screenConnectionId != null && screenConnectionId.Length != 0)
            {
                await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReceiveScreenFooter", footer);
            }

            return footer;
        }

        [HttpPut]
        public async Task<ActionResult<ScreenFooterForReturnDto>> Update(ScreenFooterForCreationDto updateDto)
        {
            var footer = await screenFooterService.Update(updateDto);
            var screenConnectionId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(footer.ScreenId);
            if (screenConnectionId != null && screenConnectionId.Length != 0)
            {
                await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReceiveScreenFooter", footer);
            }

            return footer;
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<ScreenFooterForReturnDto>> Delete(int itemId)
        {
            return await screenFooterService.Delete(itemId);
        }
    }
}
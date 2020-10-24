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
    public class ScreenHeadersController : ControllerBase
    {
        private readonly IScreenHeaderService screenHeaderService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        public ScreenHeadersController(IScreenHeaderService screenHeaderService, 
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
            this.screenHeaderService = screenHeaderService;

        }

        [HttpGet]
        public async Task<ActionResult<List<ScreenHeaderForReturnDto>>> List()
        {
            return await screenHeaderService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ScreenHeaderForReturnDto>> Create(ScreenHeaderForCreationDto createDto)
        {
            var header= await screenHeaderService.Create(createDto);
            var screenConnectionId=await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(header.ScreenId);
            if(screenConnectionId!=null && screenConnectionId.Length!=0)
            {
                await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReceiveScreenHeader",header);
            }

            return header;
        }

        [HttpPut]
        public async Task<ActionResult<ScreenHeaderForReturnDto>> Update(ScreenHeaderForCreationDto updateDto)
        {
            var header = await screenHeaderService.Update(updateDto);
            var screenConnectionId=await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(header.ScreenId);
            if(screenConnectionId!=null && screenConnectionId.Length!=0)
            {
                await kiosksHub.Clients.Clients(screenConnectionId).SendAsync("ReceiveScreenHeader",header);
            }

            return header;
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<ScreenHeaderForReturnDto>> Delete(int itemId)
        {
            return await screenHeaderService.Delete(itemId);
        }
    }
}
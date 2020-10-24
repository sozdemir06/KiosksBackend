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
    public class ScreensController : ControllerBase
    {
        private readonly IScreenService screenService;
        private readonly IKiosksService kiosksService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        public ScreensController(IScreenService screenService,IKiosksService kiosksService,
         IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
            this.screenService = screenService;
            this.kiosksService = kiosksService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ScreenForReturnDto>>> List()
        {
            return await screenService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ScreenForReturnDto>> Create(ScreenForCreationDto createDto)
        {
            return await screenService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<ScreenForReturnDto>> Update(ScreenForCreationDto updateDto)
        {
            var screen= await screenService.Update(updateDto);
            var onlineScreens=await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(screen.Id);
            if(onlineScreens!=null && onlineScreens.Length!=0)
            {
                var screenForKiosks=await kiosksService.GetScreenByIdAsync(screen.Id);
                if(screenForKiosks!=null)
                {
                    await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveScreen",screenForKiosks);
                }
            }
            return screen;
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<ScreenForReturnDto>> Delete(int itemId)
        {
            return await screenService.Delete(itemId);
        }
    }
}
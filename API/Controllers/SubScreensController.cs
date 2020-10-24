using System.Collections.Generic;
using System.Threading.Tasks;
using API.Hubs;
using Business.Abstract;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubScreensController : ControllerBase
    {
        private readonly ISubScreenService subScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;

        public SubScreensController(ISubScreenService subScreenService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.subScreenService = subScreenService;
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
        }


        [HttpGet]
        public async Task<ActionResult<List<SubScreenForReturnDto>>> List()
        {
            return await subScreenService.GetListAsync();
        }

      
        [HttpPost]
        public async Task<ActionResult<SubScreenForReturnDto>> Create(SubScreenForCreationDto createDto)
        {
            var subscreen= await subScreenService.Create(createDto);
            var onlineScreens=await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if(onlineScreens!=null && onlineScreens.Length!=0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveSubScreen",subscreen);
            }
            return subscreen;
        }


    
        [HttpPut]
        public async Task<ActionResult<SubScreenForReturnDto>> Update(SubScreenForCreationDto updateDto)
        {
            var subscreen= await subScreenService.Update(updateDto);
             var onlineScreens=await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if(onlineScreens!=null && onlineScreens.Length!=0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveSubScreen",subscreen);
            }
            return subscreen;
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<SubScreenForReturnDto>> Delete(int itemId)
        {
            return await subScreenService.Delete(itemId);
        }

        [HttpGet("{screenId}")]
        public async  Task<ActionResult<List<SubScreenForReturnDto>>> GetSubScreenListByScreenId(int screenId)
        {
            return await subScreenService.GetByScreenId(screenId);
        }
    }
}
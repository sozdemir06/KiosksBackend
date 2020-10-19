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
    public class FoodMenuSubScreenController : ControllerBase
    {
        private readonly IFoodMenuSubScreenService foodMenuSubScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IOnlineScreenService onlineScreenService;
        public FoodMenuSubScreenController(IFoodMenuSubScreenService foodMenuSubScreenService,
        IOnlineScreenService onlineScreenService,
        IHubContext<KiosksHub> kiosksHub)
        {
            this.onlineScreenService = onlineScreenService;
            this.kiosksHub = kiosksHub;
            this.foodMenuSubScreenService = foodMenuSubScreenService;

        }

        [HttpPost]
        public async Task<ActionResult<FoodMenuSubScreenForReturnDto>> Create(FoodMenuSubScreenForCreationDto creationDto)
        {
            var subscreen = await foodMenuSubScreenService.Create(creationDto);
            var onlineScreensByScreenId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensByScreenId != null && onlineScreensByScreenId.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreensByScreenId).SendAsync("ReceiveFoodMenuSubScreen", subscreen, "create");
            }
            return subscreen;

        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<FoodMenuSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await foodMenuSubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<FoodMenuSubScreenForReturnDto>> Delete(int Id)
        {
            var subscreen = await foodMenuSubScreenService.Delete(Id);
            var onlineScreensByScreenId = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(subscreen.ScreenId);
            if (onlineScreensByScreenId != null && onlineScreensByScreenId.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreensByScreenId).SendAsync("ReceiveFoodMenuSubScreen", subscreen, "delete");
            }
            return subscreen;
        }
    }
}
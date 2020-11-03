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
    public class FoodMenuController : ControllerBase
    {
        private readonly IFoodMenuService foodMenuService;
        private readonly UserTracker userTracker;
        private readonly IKiosksService kiosksService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IHubContext<AdminHub> hubContext;
        public FoodMenuController(IFoodMenuService foodMenuService, UserTracker userTracker,
        IKiosksService kiosksService,
        IOnlineScreenService onlineScreenService,
        IHubContext<KiosksHub> kiosksHub,
            IHubContext<AdminHub> hubContext)
        {
            this.hubContext = hubContext;
            this.foodMenuService = foodMenuService;
            this.userTracker = userTracker;
            this.kiosksService = kiosksService;
            this.onlineScreenService = onlineScreenService;
            this.kiosksHub = kiosksHub;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<FoodMenuForReturnDto>>> List([FromQuery] FoodMenuParams queryParams)
        {
            return await foodMenuService.GetListAsync(queryParams);
        }

        [HttpGet("detail/{announceId}")]
        public async Task<ActionResult<FoodMenuForDetailDto>> Detail(int announceId)
        {
            return await foodMenuService.GetDetailAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<FoodMenuForReturnDto>> Create([FromBody] FoodMenuForCreationDto creationDto)
        {
            var foodMenu = await foodMenuService.Create(creationDto);
            var connIds = await userTracker.GetOnlineUser();
            if (connIds != null && connIds.Length != 0)
            {
                await hubContext.Clients.GroupExcept("FoodMenu", connIds).SendAsync("ReceiveNewFoodMenu", foodMenu, true);
            }

            return foodMenu;
        }

        [HttpPut]
        public async Task<ActionResult<FoodMenuForReturnDto>> Update(FoodMenuForCreationDto updateDto)
        {
            var foodMenu = await foodMenuService.Update(updateDto);
            var connIds = await userTracker.GetOnlineUser();
            if (connIds != null && connIds.Length != 0)
            {
                await hubContext.Clients.GroupExcept("FoodMenu", connIds).SendAsync("ReceiveUpdateFoodMenu", foodMenu);
            }
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {

                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReloadScreen", true);

            }

            return foodMenu;
        }

        [HttpPut("publish")]
        public async Task<ActionResult<FoodMenuForReturnDto>> Publish(FoodMenuForCreationDto updateDto)
        {
            var foodMenu = await foodMenuService.Publish(updateDto);
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {

                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReloadScreen",true);

            }

            return foodMenu;

        }

        [HttpDelete("{announceId}")]
        public async Task<ActionResult<FoodMenuForReturnDto>> Delete(int announceId)
        {
            return await foodMenuService.Delete(announceId);
        }

    }
}
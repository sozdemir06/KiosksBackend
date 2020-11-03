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
    public class FoodMenuPhotoController : ControllerBase
    {
        private readonly IFoodMenuPhotoService foodMenuPhotoService;
        private readonly UserTracker userTracker;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IHubContext<AdminHub> hubContext;
      

        public FoodMenuPhotoController(IFoodMenuPhotoService foodMenuPhotoService,UserTracker userTracker,
        IOnlineScreenService onlineScreenService,
        IHubContext<KiosksHub> kiosksHub,
        IHubContext<AdminHub> hubContext)
        {
            this.foodMenuPhotoService = foodMenuPhotoService;
            this.userTracker = userTracker;
            this.onlineScreenService = onlineScreenService;
            this.kiosksHub = kiosksHub;
            this.hubContext = hubContext;
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<FoodMenuPhotoForReturnDto>>> List(int announceId)
        {
            return await foodMenuPhotoService.GetListAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<FoodMenuPhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            var photo = await foodMenuPhotoService.Create(uploadDto);
             var connIds = await userTracker.GetOnlineUser();
            if (connIds!=null && connIds.Length!=0)
            {
                await hubContext.Clients.GroupExcept("FoodMenu", connIds).SendAsync("ReceiveFoodMenuPhoto", photo, "create",true);
            }

            return photo;
        }

        [HttpPut]
        public async Task<FoodMenuPhotoForReturnDto> Update(FoodMenuPhotoForCreationDto creationDto)
        {
            var photo = await foodMenuPhotoService.Update(creationDto);
              var connIds = await userTracker.GetOnlineUser();
            if (connIds!=null && connIds.Length!=0)
            {
                await hubContext.Clients.GroupExcept("FoodMenu", connIds).SendAsync("ReceiveFoodMenuPhoto", photo, "update");
            }

            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
               await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReloadScreen",true);
            }

            return photo;
        }

        [HttpDelete("{photoId}")]
        public async Task<FoodMenuPhotoForReturnDto> Delete(int photoId)
        {
            var photo = await foodMenuPhotoService.Delete(photoId);
             var connIds = await userTracker.GetOnlineUser();
            if (connIds!=null && connIds.Length!=0)
            {
                await hubContext.Clients.GroupExcept("FoodMenu", connIds).SendAsync("ReceiveFoodMenuPhoto", photo, "delete");
            }
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
               await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReloadScreen",true);
            }

            return photo;
        }
    }
}
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
    public class FoodMenuBgPhotosController : ControllerBase
    {
        private readonly IFoodMenuBgPhotoService foodMenuBgPhotoService;
        private readonly UserTracker userTracker;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        private readonly IHubContext<AdminHub> hubContext;

        public FoodMenuBgPhotosController(IFoodMenuBgPhotoService foodMenuBgPhotoService, UserTracker userTracker,
        IOnlineScreenService onlineScreenService,
        IHubContext<KiosksHub> kiosksHub,
        IHubContext<AdminHub> hubContext)
        {
            this.foodMenuBgPhotoService = foodMenuBgPhotoService;
            this.userTracker = userTracker;
            this.onlineScreenService = onlineScreenService;
            this.kiosksHub = kiosksHub;
            this.hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<FoodMenuBgPhotoForReturnDto>>> List()
        {
            return await foodMenuBgPhotoService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<FoodMenuBgPhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            return await foodMenuBgPhotoService.Create(uploadDto);
        }

        [HttpPut]
        public async Task<FoodMenuBgPhotoForReturnDto> Update(FoodMenuBgPhotoForCreationDto creationDto)
        {
            var photo = await foodMenuBgPhotoService.Update(creationDto);
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReloadScreen", true);
            }

            return photo;
        }

        [HttpPut("setbg")]
        public async Task<FoodMenuBgPhotoForReturnDto> SetBackground(FoodMenuBgPhotoForCreationDto creationDto)
        {
            var photo = await foodMenuBgPhotoService.SetBackgroundPhoto(creationDto);
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReloadScreen", true);
            }

            return photo;
        }

        [HttpDelete("{photoId}")]
        public async Task<FoodMenuBgPhotoForReturnDto> Delete(int photoId)
        {
            var photo = await foodMenuBgPhotoService.Delete(photoId);
            var onlineScreens = await onlineScreenService.GetAllOnlineScreenConnectionId();
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReloadScreen", true);
            }

            return photo;
        }

    }
}
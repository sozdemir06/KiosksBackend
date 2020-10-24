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
    public class ScreenHeaderPhotosController : ControllerBase
    {
        private readonly IScreenHeaderPhotoService screenHeaderPhotoService;
        private readonly IOnlineScreenService onlineScreenService;
        private readonly IHubContext<KiosksHub> kiosksHub;
        public ScreenHeaderPhotosController(IScreenHeaderPhotoService screenHeaderPhotoService,
        IHubContext<KiosksHub> kiosksHub,
        IOnlineScreenService onlineScreenService)
        {
            this.kiosksHub = kiosksHub;
            this.onlineScreenService = onlineScreenService;
            this.screenHeaderPhotoService = screenHeaderPhotoService;

        }

        [HttpGet("{screenId}")]
        public async Task<ActionResult<List<ScreenHeaderPhotoForReturnDto>>> List(int screenId)
        {
            return await screenHeaderPhotoService.GetListAsync(screenId);
        }

        [HttpPost]
        public async Task<ActionResult<ScreenHeaderPhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            return await screenHeaderPhotoService.Create(uploadDto);
        }

        [HttpPut]
        public async Task<ScreenHeaderPhotoForReturnDto> Update(ScreenHeaderPhotoForCreationDto creationDto)
        {
            return await screenHeaderPhotoService.Update(creationDto);
        }

        [HttpPut("setmain")]
        public async Task<ScreenHeaderPhotoForReturnDto> SetMain(ScreenHeaderPhotoForCreationDto creationDto)
        {
            var photo = await screenHeaderPhotoService.SetMain(creationDto);
            var onlineScreens = await onlineScreenService.GetOnlineScreenConnectionIdByScreenId(photo.ScreenId);
            if (onlineScreens != null && onlineScreens.Length != 0)
            {
                await kiosksHub.Clients.Clients(onlineScreens).SendAsync("ReceiveScreenHeaderPhoto", photo);
            }
            return photo;
        }

        [HttpDelete("{photoId}")]
        public async Task<ScreenHeaderPhotoForReturnDto> Delete(int photoId)
        {
            return await screenHeaderPhotoService.Delete(photoId);
        }


    }
}
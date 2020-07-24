using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAnnouncePhotoController : ControllerBase
    {
        private readonly IHomeAnnouncePhotoService photoService;
        public HomeAnnouncePhotoController(IHomeAnnouncePhotoService photoService)
        {
            this.photoService = photoService;

        }


        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<HomeAnnouncePhotoForReturnDto>>> List(int announceId)
        {
            return await photoService.GetListAsync(announceId);
        }
        
        [HttpPost]
        public async Task<ActionResult<HomeAnnouncePhotoForReturnDto>> Create([FromForm]FileUploadDto uploadDto)
        {
            return await photoService.Create(uploadDto);
        }

        [HttpPut]
        public async Task<HomeAnnouncePhotoForReturnDto> Update(HomeAnnouncePhotoForCreationDto creationDto)
        {
            return await photoService.Update(creationDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<HomeAnnouncePhotoForReturnDto> Delete(int photoId)
        {
            return await photoService.Delete(photoId);
        }
    }
}
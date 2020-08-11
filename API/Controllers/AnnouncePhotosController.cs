using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncePhotosController : ControllerBase
    {
        private readonly IAnnouncePhotoService announcePhotoService;
        public AnnouncePhotosController(IAnnouncePhotoService announcePhotoService)
        {
            this.announcePhotoService = announcePhotoService;

        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<AnnouncePhotoForReturnDto>>> List(int announceId)
        {
            return await announcePhotoService.GetListAsync(announceId);
        }
        
        [HttpPost]
        public async Task<ActionResult<AnnouncePhotoForReturnDto>> Create([FromForm]FileUploadDto uploadDto)
        {
            return await announcePhotoService.Create(uploadDto);
        }

        [HttpPut]
        public async Task<AnnouncePhotoForReturnDto> Update(AnnouncePhotoForCretionDto creationDto)
        {
            return await announcePhotoService.Update(creationDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<AnnouncePhotoForReturnDto> Delete(int photoId)
        {
            return await announcePhotoService.Delete(photoId);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenHeaderPhotosController : ControllerBase
    {
        private readonly IScreenHeaderPhotoService screenHeaderPhotoService;
        public ScreenHeaderPhotosController(IScreenHeaderPhotoService screenHeaderPhotoService)
        {
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
            return await screenHeaderPhotoService.SetMain(creationDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<ScreenHeaderPhotoForReturnDto> Delete(int photoId)
        {
            return await screenHeaderPhotoService.Delete(photoId);
        }


    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsPhotoController : ControllerBase
    {
        private readonly INewsPhotoService newsPhotoService;
        public NewsPhotoController(INewsPhotoService newsPhotoService)
        {
            this.newsPhotoService = newsPhotoService;

        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<NewsPhotoForReturnDto>>> List(int announceId)
        {
            return await newsPhotoService.GetListAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<NewsPhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            return await newsPhotoService.Create(uploadDto);
        }

        [HttpPut]
        public async Task<NewsPhotoForReturnDto> Update(NewsPhotoForCreationDto creationDto)
        {
            return await newsPhotoService.Update(creationDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<NewsPhotoForReturnDto> Delete(int photoId)
        {
            return await newsPhotoService.Delete(photoId);
        }
    }
}
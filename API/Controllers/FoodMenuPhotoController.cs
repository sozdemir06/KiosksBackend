using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodMenuPhotoController : ControllerBase
    {
        private readonly IFoodMenuPhotoService foodMenuPhotoService;
        public FoodMenuPhotoController(IFoodMenuPhotoService foodMenuPhotoService)
        {
            this.foodMenuPhotoService = foodMenuPhotoService;

        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<FoodMenuPhotoForReturnDto>>> List(int announceId)
        {
            return await foodMenuPhotoService.GetListAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<FoodMenuPhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            return await foodMenuPhotoService.Create(uploadDto);
        }

        [HttpPut]
        public async Task<FoodMenuPhotoForReturnDto> Update(FoodMenuPhotoForCreationDto creationDto)
        {
            return await foodMenuPhotoService.Update(creationDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<FoodMenuPhotoForReturnDto> Delete(int photoId)
        {
            return await foodMenuPhotoService.Delete(photoId);
        }
    }
}
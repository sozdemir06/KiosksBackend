using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodMenuBgPhotosController : ControllerBase
    {
        private readonly IFoodMenuBgPhotoService foodMenuBgPhotoService;
        public FoodMenuBgPhotosController(IFoodMenuBgPhotoService foodMenuBgPhotoService)
        {
            this.foodMenuBgPhotoService = foodMenuBgPhotoService;

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
            return await foodMenuBgPhotoService.Update(creationDto);
        }

        [HttpPut("setbg")]
         public async Task<FoodMenuBgPhotoForReturnDto> SetBackground(FoodMenuBgPhotoForCreationDto creationDto)
        {
            return await foodMenuBgPhotoService.SetBackgroundPhoto(creationDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<FoodMenuBgPhotoForReturnDto> Delete(int photoId)
        {
            return await foodMenuBgPhotoService.Delete(photoId);
        }

    }
}
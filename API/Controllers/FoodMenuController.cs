using System.Threading.Tasks;
using Business.Abstract;
using Business.Helpers;
using Core.QueryParams;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodMenuController : ControllerBase
    {
        private readonly IFoodMenuService foodMenuService;
        public FoodMenuController(IFoodMenuService foodMenuService)
        {
            this.foodMenuService = foodMenuService;

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
            return await foodMenuService.Create(creationDto);
        }

        [HttpPut]
        public async Task<ActionResult<FoodMenuForReturnDto>> Update(FoodMenuForCreationDto updateDto)
        {
            return await foodMenuService.Update(updateDto);
        }

        [HttpPut("publish")]
        public async Task<ActionResult<FoodMenuForReturnDto>> Publish(FoodMenuForCreationDto updateDto)
        {
            return await foodMenuService.Publish(updateDto);
        }

        [HttpDelete("{announceId}")]
        public async Task<ActionResult<FoodMenuForReturnDto>> Delete(int announceId)
        {
            return await foodMenuService.Delete(announceId);
        }

    }
}
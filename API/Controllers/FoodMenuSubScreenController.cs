using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodMenuSubScreenController : ControllerBase
    {
        private readonly IFoodMenuSubScreenService foodMenuSubScreenService;
        public FoodMenuSubScreenController(IFoodMenuSubScreenService foodMenuSubScreenService)
        {
            this.foodMenuSubScreenService = foodMenuSubScreenService;

        }

        [HttpPost]
        public async Task<ActionResult<FoodMenuSubScreenForReturnDto>> Create(FoodMenuSubScreenForCreationDto creationDto)
        {
            return await foodMenuSubScreenService.Create(creationDto);
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<FoodMenuSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await foodMenuSubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<FoodMenuSubScreenForReturnDto>> Delete(int Id)
        {
            return await foodMenuSubScreenService.Delete(Id);
        }
    }
}
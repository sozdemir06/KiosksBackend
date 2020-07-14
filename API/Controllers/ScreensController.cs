using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreensController : ControllerBase
    {
        private readonly IScreenService screenService;
        public ScreensController(IScreenService screenService)
        {
            this.screenService = screenService;

        }


        [HttpGet]
        public async Task<ActionResult<List<ScreenForReturnDto>>> List()
        {
            return await  screenService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ScreenForReturnDto>> Create(ScreenForCreationDto createDto)
        {
            return await screenService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<ScreenForReturnDto>> Update(ScreenForCreationDto updateDto)
        {
            return await screenService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<ScreenForReturnDto>> Delete(int itemId)
        {
            return await screenService.Delete(itemId);
        }
    }
}
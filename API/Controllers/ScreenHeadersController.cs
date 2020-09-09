using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenHeadersController : ControllerBase
    {
        private readonly IScreenHeaderService screenHeaderService;
        public ScreenHeadersController(IScreenHeaderService screenHeaderService)
        {
            this.screenHeaderService = screenHeaderService;

        }

        [HttpGet]
        public async Task<ActionResult<List<ScreenHeaderForReturnDto>>> List()
        {
            return await screenHeaderService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ScreenHeaderForReturnDto>> Create(ScreenHeaderForCreationDto createDto)
        {
            return await screenHeaderService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<ScreenHeaderForReturnDto>> Update(ScreenHeaderForCreationDto updateDto)
        {
            return await screenHeaderService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<ScreenHeaderForReturnDto>> Delete(int itemId)
        {
            return await screenHeaderService.Delete(itemId);
        }
    }
}
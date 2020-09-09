using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenFootersController : ControllerBase
    {
        private readonly IScreenFooterService screenFooterService;
        public ScreenFootersController(IScreenFooterService screenFooterService)
        {
            this.screenFooterService = screenFooterService;

        }

         [HttpGet]
        public async Task<ActionResult<List<ScreenFooterForReturnDto>>> List()
        {
            return await screenFooterService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ScreenFooterForReturnDto>> Create(ScreenFooterForCreationDto createDto)
        {
            return await screenFooterService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<ScreenFooterForReturnDto>> Update(ScreenFooterForCreationDto updateDto)
        {
            return await screenFooterService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<ScreenFooterForReturnDto>> Delete(int itemId)
        {
            return await screenFooterService.Delete(itemId);
        }
    }
}
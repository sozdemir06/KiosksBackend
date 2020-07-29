using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAnnounceSubScreensController : ControllerBase
    {
        private readonly IHomeAnnounceSubScreenService homeAnnounceSubScreenService;
        public HomeAnnounceSubScreensController(IHomeAnnounceSubScreenService homeAnnounceSubScreenService)
        {
            this.homeAnnounceSubScreenService = homeAnnounceSubScreenService;

        }

        [HttpPost]
        public async Task<ActionResult<HomeAnnounceSubScreenForReturnDto>> Create(HomeAnnounceSubScreenForCreationDto creationDto)
        {
            return await homeAnnounceSubScreenService.Create(creationDto);
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<HomeAnnounceSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await homeAnnounceSubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<HomeAnnounceSubScreenForReturnDto>> Delete(int Id)
        {
            return await homeAnnounceSubScreenService.Delete(Id);
        }
    }
}
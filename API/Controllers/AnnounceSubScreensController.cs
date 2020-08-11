using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnounceSubScreensController : ControllerBase
    {
        private readonly IAnnounceSubScreenService announceSubScreenService;
        public AnnounceSubScreensController(IAnnounceSubScreenService announceSubScreenService)
        {
            this.announceSubScreenService = announceSubScreenService;

        }

        [HttpPost]
        public async Task<ActionResult<AnnounceSubScreenForReturnDto>> Create(AnnounceSubScreenForCreationDto creationDto)
        {
            return await announceSubScreenService.Create(creationDto);
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<AnnounceSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await announceSubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<AnnounceSubScreenForReturnDto>> Delete(int Id)
        {
            return await announceSubScreenService.Delete(Id);
        }
    }
}
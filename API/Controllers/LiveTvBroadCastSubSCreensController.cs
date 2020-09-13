using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveTvBroadCastSubSCreensController : ControllerBase
    {
        private readonly ILiveTvBroadCastSubScreenService liveTvBroadCastSubSreenService;
        public LiveTvBroadCastSubSCreensController(ILiveTvBroadCastSubScreenService liveTvBroadCastSubSreenService)
        {
            this.liveTvBroadCastSubSreenService = liveTvBroadCastSubSreenService;

        }

         [HttpPost]
        public async Task<ActionResult<LiveTvBroadCastSubScreenForReturnDto>> Create(LiveTvBroadCastSubScreenForCreationDto creationDto)
        {
            return await liveTvBroadCastSubSreenService.Create(creationDto);
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<LiveTvBroadCastSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await liveTvBroadCastSubSreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<LiveTvBroadCastSubScreenForReturnDto>> Delete(int Id)
        {
            return await liveTvBroadCastSubSreenService.Delete(Id);
        }
    }
}
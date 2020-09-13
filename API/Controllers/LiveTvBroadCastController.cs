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
    public class LiveTvBroadCastController : ControllerBase
    {
        private readonly ILiveTvBroadCastService liveTvBroadCastService;
        public LiveTvBroadCastController(ILiveTvBroadCastService liveTvBroadCastService)
        {
            this.liveTvBroadCastService = liveTvBroadCastService;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<LiveTvBroadCastForReturnDto>>> List([FromQuery]LiveTvBroadCastParams queryParams)
        {
            return await liveTvBroadCastService.GetListAsync(queryParams);
        }

        [HttpGet("detail/{announceId}")]
        public async Task<ActionResult<LiveTvBroadCastForDetailDto>> Detail(int announceId)
        {
            return await liveTvBroadCastService.GetDetailAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<LiveTvBroadCastForReturnDto>> Create([FromBody]LiveTvBroadCastForCreationDto creationDto)
        {
            return await liveTvBroadCastService.Create(creationDto);
        }

        [HttpPut]
        public async Task<ActionResult<LiveTvBroadCastForReturnDto>> Update(LiveTvBroadCastForCreationDto updateDto)
        {
            return await liveTvBroadCastService.Update(updateDto);
        }

        [HttpPut("publish")]
        public async Task<ActionResult<LiveTvBroadCastForReturnDto>> Publish(LiveTvBroadCastForCreationDto updateDto)
        {
            return await liveTvBroadCastService.Publish(updateDto);
        }

        [HttpDelete("{announceId}")]
        public async Task<ActionResult<LiveTvBroadCastForReturnDto>> Delete(int announceId)
        {
            return await liveTvBroadCastService.Delete(announceId);
        }
    }
}
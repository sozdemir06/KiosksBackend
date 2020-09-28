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
    public class AnnouncesController : ControllerBase
    {
        private readonly IAnnounceService announceService;
        public AnnouncesController(IAnnounceService announceService)
        {
            this.announceService = announceService;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<AnnounceForReturnDto>>> List([FromQuery] AnnounceParams queryParams)
        {
            return await announceService.GetListAsync(queryParams);
        }

        [HttpGet("detail/{announceId}")]
        public async Task<ActionResult<AnnounceForDetailDto>> Detail(int announceId)
        {
            return await announceService.GetDetailAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<AnnounceForReturnDto>> Create([FromBody] AnnounceForCreationDto creationDto)
        {
            return await announceService.Create(creationDto);
        }

        [HttpPut]
        public async Task<ActionResult<AnnounceForReturnDto>> Update(AnnounceForCreationDto updateDto)
        {
            return await announceService.Update(updateDto);
        }

        [HttpPut("publish")]
        public async Task<ActionResult<AnnounceForReturnDto>> Publish(AnnounceForCreationDto updateDto)
        {
            return await announceService.Publish(updateDto);
        }

        [HttpDelete("{announceId}")]
        public async Task<ActionResult<AnnounceForReturnDto>> Delete(int announceId)
        {
            return await announceService.Delete(announceId);
        }

        [HttpPost("createforuser/{userId}")]
        public async Task<ActionResult<AnnounceForReturnDto>> CreateForPublicAsync([FromBody] AnnounceForCreationDto creationDto,int userId)
        {
            return await announceService.Create(creationDto);
        }

        [HttpPut("updateforuser/{userId}")]
        public async Task<ActionResult<AnnounceForReturnDto>> Update(AnnounceForCreationDto updateDto,int userId)
        {
            return await announceService.Update(updateDto);
        }



    }
}
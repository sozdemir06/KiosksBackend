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
    public class HomeAnnouncesController : ControllerBase
    {
        private readonly IHomeAnnounceService homeAnnounceService;
        public HomeAnnouncesController(IHomeAnnounceService homeAnnounceService)
        {
            this.homeAnnounceService = homeAnnounceService;

        }


        [HttpGet]
        public async Task<ActionResult<Pagination<HomeAnnounceForReturnDto>>> List([FromQuery] HomeAnnounceParams queryParams)
        {
            return await homeAnnounceService.GetListAsync(queryParams);
        }

        [HttpGet("detail/{announceId}")]
        public async Task<ActionResult<HomeAnnounceForDetailDto>> Detail(int announceId)
        {
            return await homeAnnounceService.GetDetailAsync(announceId);
        }

        [HttpPost]
        public async Task<ActionResult<HomeAnnounceForReturnDto>> Create([FromBody] HomeAnnounceForCreationDto creationDto)
        {
            return await homeAnnounceService.Create(creationDto);
        }

        [HttpPut]
        public async Task<ActionResult<HomeAnnounceForReturnDto>> Update(HomeAnnounceForCreationDto updateDto)
        {
            return await homeAnnounceService.Update(updateDto);
        }

        [HttpPut("publish")]
        public async Task<ActionResult<HomeAnnounceForReturnDto>> Publish(HomeAnnounceForCreationDto updateDto)
        {
            return await homeAnnounceService.Publish(updateDto);
        }

        [HttpDelete("{announceId}")]
        public async Task<ActionResult<HomeAnnounceForReturnDto>> Delete(int announceId)
        {
            return await homeAnnounceService.Delete(announceId);
        }

        [HttpPost("createforuser/{userId}")]
        public async Task<ActionResult<HomeAnnounceForUserDto>> CreateForUser([FromBody] HomeAnnounceForCreationDto creationDto, int userId)
        {
            return await homeAnnounceService.CreateForPublicAsync(creationDto, userId);
        }

        [HttpPut("updateforuser/{userId}")]
        public async Task<ActionResult<HomeAnnounceForUserDto>> Update(HomeAnnounceForCreationDto updateDto,int userId)
        {
            return await homeAnnounceService.UpdateForPublicAsync(updateDto,userId);
        }
    }
}
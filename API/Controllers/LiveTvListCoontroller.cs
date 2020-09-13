using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveTvListController : ControllerBase
    {
        private readonly ILiveTvListService liveTvListService;
        public LiveTvListController(ILiveTvListService liveTvListService)
        {
            this.liveTvListService = liveTvListService;

        }

        [HttpGet]
        public async Task<ActionResult<List<LiveTvListForReturnDto>>> List()
        {
            return await liveTvListService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<LiveTvListForReturnDto>> Create(LiveTvListForCreationDto createDto)
        {
            return await liveTvListService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<LiveTvListForReturnDto>> Update(LiveTvListForCreationDto updateDto)
        {
            return await liveTvListService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<LiveTvListForReturnDto>> Delete(int itemId)
        {
            return await liveTvListService.Delete(itemId);
        }
    }
}
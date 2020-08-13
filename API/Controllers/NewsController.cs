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
    public class NewsController : ControllerBase
    {
        private readonly INewsService newsService;
        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;

        }

         [HttpGet]
        public  async Task<ActionResult<Pagination<NewsForReturnDto>>> List([FromQuery]NewsParams queryParams)
        {
            return await newsService.GetListAsync(queryParams);
        }

        [HttpGet("detail/{announceId}")]
        public  async Task<ActionResult<NewsForDetailDto>> Detail(int announceId)
        {
            return await newsService.GetDetailAsync(announceId);
        }

        [HttpPost]
        public  async Task<ActionResult<NewsForReturnDto>> Create([FromBody]NewsForCreationDto creationDto)
        {
            return await newsService.Create(creationDto);
        }

        [HttpPut]
        public  async Task<ActionResult<NewsForReturnDto>> Update(NewsForCreationDto updateDto)
        {
            return await newsService.Update(updateDto);
        }

        [HttpPut("publish")]
        public  async Task<ActionResult<NewsForReturnDto>> Publish(NewsForCreationDto updateDto)
        {
            return await newsService.Publish(updateDto);
        }

        [HttpDelete("{announceId}")]
        public  async Task<ActionResult<NewsForReturnDto>> Delete(int announceId)
        {
            return await newsService.Delete(announceId);
        }
    }
}
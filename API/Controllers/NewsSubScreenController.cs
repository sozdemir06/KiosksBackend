using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsSubScreenController : ControllerBase
    {
        private readonly INewsSubScreenService newsSubScreenService;
        public NewsSubScreenController(INewsSubScreenService newsSubScreenService)
        {
            this.newsSubScreenService = newsSubScreenService;
        }

        [HttpPost]
        public async Task<ActionResult<NewsSubScreenForReturnDto>> Create(NewsSubScreenForCreationDto creationDto)
        {
            return await newsSubScreenService.Create(creationDto);
        }

        [HttpGet("{announceId}")]
        public async Task<ActionResult<List<NewsSubScreenForReturnDto>>> GetByAnnounceId(int announceId)
        {
            return await newsSubScreenService.GetByAnnounceId(announceId);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<NewsSubScreenForReturnDto>> Delete(int Id)
        {
            return await newsSubScreenService.Delete(Id);
        }
    }
}
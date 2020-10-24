using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KiosksController : ControllerBase
    {
        private readonly IKiosksService kiosksService;
        public KiosksController(IKiosksService kiosksService)
        {
            this.kiosksService = kiosksService;

        }

        [HttpGet("{screenId}")]
        public async Task<ActionResult<KiosksForReturnDto>> Kiosks(int screenId)
        {
            return await kiosksService.KiosksAsync(screenId);
        }

         [HttpGet("subscreen/{subscreenId}")]
        public async Task<ActionResult<KiosksForReturnDto>> KiosksbySubScreenId(int subscreenId)
        {
            return await kiosksService.KiosksBySubscreenId(subscreenId);
        }


    }
}
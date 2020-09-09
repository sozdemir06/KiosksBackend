using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WheatherForeCastsController : ControllerBase
    {
        private readonly IWheatherForeCastService wheatherForeCastService;
        public WheatherForeCastsController(IWheatherForeCastService wheatherForeCastService)
        {
            this.wheatherForeCastService = wheatherForeCastService;

        }

        [HttpGet]
        public async Task<List<WheatherForeCastForReturnDto>> GetWheather()
        {
            return await wheatherForeCastService.WheatherForeCastsAsync();
        }
    }
}
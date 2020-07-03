using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsOfHomeController : ControllerBase
    {
        private readonly IFlatOfHomeService flatOfHomeService;
        public FlatsOfHomeController(IFlatOfHomeService flatOfHomeService)
        {
            this.flatOfHomeService = flatOfHomeService;

        }
        [HttpGet]
        public async Task<ActionResult<List<FlatOfHomeForReturnDto>>> List()
        {
            return await flatOfHomeService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<FlatOfHomeForReturnDto>> Create(FlatOfHomeForCreationDto createDto)
        {
            return await flatOfHomeService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<FlatOfHomeForReturnDto>> Update(FlatOfHomeForCreationDto updateDto)
        {
            return await flatOfHomeService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<FlatOfHomeForReturnDto>> Delete(int itemId)
        {
            return await flatOfHomeService.Delete(itemId);
        }
    }
}
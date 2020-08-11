using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnounceContentTypeController : ControllerBase
    {
        private readonly IAnnounceContentTypeService announcecontentTypeService;
        public AnnounceContentTypeController(IAnnounceContentTypeService announcecontentTypeService)
        {
            this.announcecontentTypeService = announcecontentTypeService;

        }


        [HttpGet]
        public async Task<ActionResult<List<AnnounceContentTypeForReturnDto>>> List()
        {
            return await announcecontentTypeService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AnnounceContentTypeForReturnDto>> Create(AnnounceContentTypeForCreationDto createDto)
        {
            return await announcecontentTypeService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<AnnounceContentTypeForReturnDto>> Update(AnnounceContentTypeForCreationDto updateDto)
        {
            return await announcecontentTypeService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<AnnounceContentTypeForReturnDto>> Delete(int itemId)
        {
            return await announcecontentTypeService.Delete(itemId);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyGroupController : ControllerBase
    {
        private readonly INotifyGroupService notifyGroupService;
        public NotifyGroupController(INotifyGroupService notifyGroupService)
        {
            this.notifyGroupService = notifyGroupService;

        }

        [HttpGet]
        public async Task<ActionResult<List<NotifyGroupForReturnDto>>> List()
        {
            return await notifyGroupService.GetListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<NotifyGroupForReturnDto>> Create(NotifyGroupForCreationDto createDto)
        {
            return await notifyGroupService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<NotifyGroupForReturnDto>> Update(NotifyGroupForCreationDto updateDto)
        {
            return await notifyGroupService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<NotifyGroupForReturnDto>> Delete(int itemId)
        {
            return await notifyGroupService.Delete(itemId);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNotifyGroupsController : ControllerBase
    {
        private readonly IUserNotifyGroupService userNotifyService;

        public UserNotifyGroupsController(IUserNotifyGroupService userNotifyService)
        {
            this.userNotifyService = userNotifyService;


        }

        [HttpGet]
        public async Task<ActionResult<List<UserNotifyGroupForReturnDto>>> List()
        {
            return await userNotifyService.GetListAsync();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<UserNotifyGroupForReturnDto>>> GetListByUserId(int userId)
        {
            return await userNotifyService.GetListByUserId(userId);
        }

        [HttpPost]
        public async Task<ActionResult<UserNotifyGroupForReturnDto>> Create(UserNotifyGroupForCreationDto createDto)
        {
            return await userNotifyService.Create(createDto);
        }

        [HttpPut]
        public async Task<ActionResult<UserNotifyGroupForReturnDto>> Update(UserNotifyGroupForCreationDto updateDto)
        {
            return await userNotifyService.Update(updateDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<UserNotifyGroupForReturnDto>> Delete(int itemId)
        {
            return await userNotifyService.Delete(itemId);
        }

    }
}
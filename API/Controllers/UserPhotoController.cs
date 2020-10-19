using System.Collections.Generic;
using System.Threading.Tasks;
using API.Hubs;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPhotoController : ControllerBase
    {
        private readonly IUserPhotoService userPhotoService;
        private readonly IHubContext<AdminHub> hubContext;
        public UserPhotoController(IUserPhotoService userPhotoService, IHubContext<AdminHub> hubContext)
        {
            this.hubContext = hubContext;
            this.userPhotoService = userPhotoService;

        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<UserPhotoForReturnDto>>> List(int userId)
        {
            return await userPhotoService.GetListAsync(userId);
        }

        [HttpPost]
        public async Task<ActionResult<UserPhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            var photo= await userPhotoService.Create(uploadDto);
            await hubContext.Clients.Group("User").SendAsync("ReceiveNewUserProfilePhoto",photo);
            return photo;
        }

        [HttpPut]
        public async Task<UserPhotoForReturnDto> Update(UserPhotoForCreationDto creationDto)
        {
            return await userPhotoService.Update(creationDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<UserPhotoForReturnDto> Delete(int photoId)
        {
            return await userPhotoService.Delete(photoId);
        }
    }
}
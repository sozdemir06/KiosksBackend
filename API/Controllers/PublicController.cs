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
    public class PublicController : ControllerBase
    {
        private readonly IPublicService publicService;
        private readonly IHubContext<AdminHub> hubContext;
        public PublicController(IPublicService publicService, IHubContext<AdminHub> hubContext)
        {
            this.hubContext = hubContext;
            this.publicService = publicService;

        }

        [HttpGet("all")]
        public async Task<ActionResult<PublicForReturnDto>> GetAllAnounces()
        {
            return await publicService.GetAllAnnounceForPublicAsync();
        }

        [HttpGet("userbyId/{userId}")]
        public async Task<ActionResult<UserForListDto>> GetUserById(int userId)
        {
            return await publicService.GetUSerById(userId);
        }

        [HttpGet("UserCamPusAndDepartmentAndDegree")]
        public async Task<ActionResult<UserCamPusAndDepartmentAndDegree>> UserCamPusAndDepartmentAndDegreeAsync()
        {
            return await publicService.UserCamPusAndDepartmentAndDegreeAsync();
        }

        [HttpPut("updateuser/{userId}")]
        public async Task<ActionResult<UserForListDto>> UpdateUser(UserForRegisterDto userForRegisterDto, int userId)
        {
            return await publicService.UpdateUserProfileAsync(userForRegisterDto, userId);
        }

        [HttpPost("changepassword/{userId}")]
        public async Task<ActionResult> ChangePassword(UserForChangePasswordDto userForChangePasswordDto, int userId)
        {
            await publicService.ChangePassword(userForChangePasswordDto, userId);
            return Ok();
        }

        [HttpPost("uploadprofilephoto")]
        public async Task<ActionResult<UserPhotoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            var photo = await publicService.UploadProfilePhoto(uploadDto);
            await hubContext.Clients.Group("User").SendAsync("ReceiveNewUserProfilePhoto",photo);
            return photo;
        }

        [HttpPut("makemainphoto/{userId}")]
        public async Task<ActionResult<UserPhotoForReturnDto>> MakeMainPhoto(UserPhotoForCreationDto creationDto, int userId)
        {
            return await publicService.MakeMainPhotoAsync(creationDto, userId);
        }

    }
}
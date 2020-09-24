using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPhotoController : ControllerBase
    {
        private readonly IUserPhotoService userPhotoService;
        public UserPhotoController(IUserPhotoService userPhotoService)
        {
            this.userPhotoService = userPhotoService;

        }

         [HttpGet("{userId}")]
        public async Task<ActionResult<List<UserPhotoForReturnDto>>> List(int userId)
        {
            return await userPhotoService.GetListAsync(userId);
        }
        
        [HttpPost]
        public async Task<ActionResult<UserPhotoForReturnDto>> Create([FromForm]FileUploadDto uploadDto)
        {
            return await userPhotoService.Create(uploadDto);
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
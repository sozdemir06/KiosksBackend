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
    public class PublicLogoController : ControllerBase
    {
        private readonly IPublicLogoService publicLogoService;
        private readonly IHubContext<AdminHub> hubContext;
        public PublicLogoController(IPublicLogoService publicLogoService, IHubContext<AdminHub> hubContext)
        {
            this.hubContext = hubContext;
            this.publicLogoService = publicLogoService;

        }

        [HttpGet]
        public async Task<ActionResult<List<PublicLogoForReturnDto>>> List(int screenId)
        {
            return await publicLogoService.GetListAsync(screenId);
        }

        [HttpGet("main")]
        public async Task<ActionResult<PublicLogoForReturnDto>> GetMain()
        {
            return await publicLogoService.GetMainLogo();
        }

        [HttpPost]
        public async Task<ActionResult<PublicLogoForReturnDto>> Create([FromForm] FileUploadDto uploadDto)
        {
            return await publicLogoService.Create(uploadDto);
        }

        [HttpPut]
        public async Task<PublicLogoForReturnDto> Update(PublicLogoForCreationDto creationDto)
        {
            return await publicLogoService.Update(creationDto);
        }

        [HttpPut("setmain")]
        public async Task<PublicLogoForReturnDto> SetMain(PublicLogoForCreationDto creationDto)
        {
            var logo = await publicLogoService.SetMain(creationDto);
            if (logo != null)
            {
                await hubContext.Clients.All.SendAsync("ReceiveLogo", logo);
            }
            return logo;
        }

        [HttpDelete("{photoId}")]
        public async Task<PublicLogoForReturnDto> Delete(int photoId)
        {
            return await publicLogoService.Delete(photoId);
        }
    }
}
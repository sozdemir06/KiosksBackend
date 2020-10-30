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
    public class PublicFooterTextController : ControllerBase
    {
        private readonly IPublicFooterTextService publicFooterTextService;
        private readonly IHubContext<AdminHub> hubContext;
        private readonly UserTracker userTracker;
        public PublicFooterTextController(IPublicFooterTextService publicFooterTextService, UserTracker userTracker,
        IHubContext<AdminHub> hubContext)
        {
            this.userTracker = userTracker;
            this.hubContext = hubContext;
            this.publicFooterTextService = publicFooterTextService;

        }

        [HttpGet]
        public async Task<ActionResult<PublicFooterTextForReturnDto>> GetFooterText()
        {
            return await publicFooterTextService.GetFooterTextAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PublicFooterTextForReturnDto>> Create(PublicFooterTextForCreationDto creationDto)
        {
            var footerText= await publicFooterTextService.Create(creationDto);
            if(footerText!=null)
            {
                var onlineUser=await userTracker.GetOnlineUser();
                if(onlineUser!=null && onlineUser.Length!=0)
                {
                    await hubContext.Clients.AllExcept(onlineUser).SendAsync("ReceiveFooterText",footerText);
                }
            }
            return footerText;
        }

    }
}
using System.Threading.Tasks;
using API.Hubs;
using Business.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        private readonly IHubContext<AdminHub> hubContext;
        public AuthController(IAuthService authService, IUserService userService, IHubContext<AdminHub> hubContext)
        {
            this.hubContext = hubContext;
            this.userService = userService;
            this.authService = authService;


        }

        [HttpPost("login")]
        public async Task<ActionResult<AccessToken>> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await authService.Login(userForLoginDto);

            var result = await authService.CreateAccessToken(userToLogin);

            return result;

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserForListDto>> Register(UserForRegisterDto userForRegisterDto)
        {
            await authService.UserExist(userForRegisterDto.Email);
            var result = await authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var user = await userService.GetUserAsync(result.Email);
            await hubContext.Clients.Group("User").SendAsync("ReceiveNewUser",user);
            return user;

        }
    }
}
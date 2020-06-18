using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
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
            return await userService.GetUserAsync(result.Email);



        }
    }
}
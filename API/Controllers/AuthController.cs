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
        public AuthController(IAuthService authService)
        {
            this.authService = authService;

        }

        [HttpPost("login")]
        public async Task<ActionResult<AccessToken>> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin=await authService.Login(userForLoginDto);

            var result=await authService.CreateAccessToken(userToLogin);

            return result;

        }

        [HttpPost("register")]
        public async Task<ActionResult<AccessToken>> Register(UserForRegisterDto userForRegisterDto)
        {
            await authService.UserExist(userForRegisterDto.Email);
            var registerResult=await authService.Register(userForRegisterDto,userForRegisterDto.Password);
            var accessToken=await authService.CreateAccessToken(registerResult);
            return accessToken;    

        } 
    }
}
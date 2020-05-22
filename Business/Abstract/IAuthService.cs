using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
         Task<User> Register(UserForRegisterDto userForRegisterDto,string password);
         Task<User> Login(UserForLoginDto userForLoginDto);
         Task UserExist(string email);
         Task<AccessToken> CreateAccessToken(User user);
    }
}
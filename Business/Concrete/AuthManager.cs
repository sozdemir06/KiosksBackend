using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidaitonRules.FluentValidation;
using Core.Aspects.AutoFac.Validation;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService userService;
        private readonly ITokenHelper tokenHelper;
        private readonly IMapper mapper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper)
        {
            this.mapper = mapper;
            this.tokenHelper = tokenHelper;
            this.userService = userService;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            var userRoles = await userService.GetUserRoles(user);
            var accesstoken = tokenHelper.CreateToken(user, userRoles);
            if (accesstoken == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { TokenNotCreated = Messages.TokenNotCreated });
            }
            return accesstoken;

        }

        [ValidationAspect(typeof(UserForLoginValidator))]
        public async Task<User> Login(UserForLoginDto userForLoginDto)
        {
            var user = await userService.GetByEmail(userForLoginDto.Email);
            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Blaaaa = Messages.UserNotFound });
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { CantAccess = Messages.WrongPassword });
            }

            return user;

        }

        [ValidationAspect(typeof(UserForRegisterValidator))]
        public async Task<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePaswordHash(password, out passwordHash, out passwordSalt);


            var user = mapper.Map<User>(userForRegisterDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await userService.Add(user);
            return user;
        }

        public async Task UserExist(string email)
        {
            if (await userService.GetByEmail(email) != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { AlreadyExist = Messages.UserAlreadyExists });
            }
        }
    }
}
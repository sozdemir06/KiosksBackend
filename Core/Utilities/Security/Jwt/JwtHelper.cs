using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Jwt.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _tokenExpire;
     

        public JwtHelper(IConfiguration configuration)
        {
            this.Configuration = configuration;
            _tokenOptions =Configuration.GetSection("TokenOptions").Get<TokenOptions>();
             _tokenExpire = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

      }
        
        public AccessToken CreateToken(User user, List<UserRoleForListDto> userRoles)
        {
            var securityKey=SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var creds=SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt=CreateJwtSecurityToken(_tokenOptions,user,creds,userRoles);
            
             var handler=new JwtSecurityTokenHandler();
             var token=handler.CreateToken(jwt);
             

             return new AccessToken
             {
                 Token=handler.WriteToken(token),
                 Expiration=_tokenExpire,
                 FirstName=user.FirstName,
                 LastName=user.LastName,
                 Email=user.Email,
                 UserId=user.Id
             };



        }

         public SecurityTokenDescriptor CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
                       SigningCredentials signingCredentials, List<UserRoleForListDto> userRoles)
        {
            var jwt = new SecurityTokenDescriptor
            {
                Issuer = tokenOptions.Issuer,
                Audience = tokenOptions.Audience,
                Expires = _tokenExpire,
                Subject = new ClaimsIdentity(SetClaim(user, userRoles)),
                SigningCredentials = signingCredentials
            };

            return jwt;

        }

         private IEnumerable<Claim> SetClaim(User user, List<UserRoleForListDto> userRoles)
        {
            var claims = new List<Claim>();
            claims.AddUserId(user.Id);
            claims.AddEmail(user.Email);
            claims.AddUserRoles(userRoles.Select(r => r.Name).ToArray());
            return claims;
        }
    }
}
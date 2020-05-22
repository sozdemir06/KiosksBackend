using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
         
         AccessToken CreateToken(User user,List<UserRoleForListDto> roles);
    }
}
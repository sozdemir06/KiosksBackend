using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddUserId(this ICollection<Claim> claims,int userId)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier,userId.ToString()));
        }

        public static void AddEmail(this ICollection<Claim> claims,string email)
        {
           claims.Add(new Claim(ClaimTypes.Email,email)); 
        }

        public static void AddUserRoles(this ICollection<Claim> claims,string[] userRoles)
        {
            userRoles.ToList().ForEach(role=>claims.Add(new Claim(ClaimTypes.Role,role)));
        }

    }
}
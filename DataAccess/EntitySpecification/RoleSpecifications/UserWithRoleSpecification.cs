using System.Linq;
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;


namespace DataAccess.EntitySpecification.RoleSpecifications
{
    public class UserWithRoleSpecification:BaseSpecification<Role>
    {
         public UserWithRoleSpecification(User user)
         :base(x=>x.UserRoles.Any(u=>u.UserId==user.Id))
         {
             AddInclude(x=>x.UserRoles);
             AddInclude(x=>x.RoleCategory);
         }

    }
}
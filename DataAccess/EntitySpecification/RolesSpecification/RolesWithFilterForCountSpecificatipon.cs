using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.RolesSpecification
{
    public class RolesWithFilterForCountSpecificatipon:BaseSpecification<Role>
    {
        public RolesWithFilterForCountSpecificatipon(RoleQueryParams queryParams)
         :base(x=>
            (string.IsNullOrEmpty(queryParams.Search) 
            || x.Name.ToLower().Contains(queryParams.Search)
            || x.RoleCategory.Name.ToLower().Contains(queryParams.Search)
            || x.Description.ToLower().Contains(queryParams.Search)
            ) &&
            (!queryParams.RoleCategoryId.HasValue || x.RoleCategoryId==queryParams.RoleCategoryId)
            
        )
        {
            
        }
    }
}
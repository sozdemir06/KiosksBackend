using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.RolesSpecification
{
    public class RolesWithRoleCategorySpecification:BaseSpecification<Role>
    {
        public RolesWithRoleCategorySpecification(RoleQueryParams queryParams)
        :base(x=>
            (string.IsNullOrEmpty(queryParams.Search) 
            || x.Name.ToLower().Contains(queryParams.Search)
            || x.RoleCategory.Name.ToLower().Contains(queryParams.Search)
            || x.Description.ToLower().Contains(queryParams.Search)
            ) &&
            (!queryParams.RoleCategoryId.HasValue || x.RoleCategoryId==queryParams.RoleCategoryId)
            
        )
        {

            AddInclude(x=>x.RoleCategory);
            ApplyPaging(queryParams.PageSize*(queryParams.PageIndex-1),queryParams.PageSize);
            
            
        }


        public RolesWithRoleCategorySpecification(int id):base(x=>x.Id==id)
        {
            AddInclude(x=>x.RoleCategory);
        }
    }
}
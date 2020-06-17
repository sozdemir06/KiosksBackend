using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.UsersSpecification
{
    public class UserWithTitleAndCampusSpesification:BaseSpecification<User>
    {
        public UserWithTitleAndCampusSpesification(UserQueryParams queryParams)
        :base(x=>
                (string.IsNullOrEmpty(queryParams.Search) 
                    || x.FirstName.ToLower().Contains(queryParams.Search)
                    || x.LastName.ToLower().Contains(queryParams.Search)
                    || x.Department.Name.ToLower().Contains(queryParams.Search)
                    || x.Campus.Name.ToLower().Contains(queryParams.Search)
                    
                    ) &&
                (!queryParams.Status.HasValue || x.IsActive ==queryParams.Status)
            )
        {
            AddInclude(x=>x.Campus);
            AddInclude(x=>x.Department);
            AddInclude(x=>x.Degree);
            AddOrderBy(x=>x.FirstName);
            ApplyPaging(queryParams.PageSize*(queryParams.PageIndex-1),queryParams.PageSize);
        }
    }
}
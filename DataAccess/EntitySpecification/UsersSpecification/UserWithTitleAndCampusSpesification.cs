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
                (string.IsNullOrEmpty(queryParams.StatusPassive) || x.IsActive ==false) &&
                (string.IsNullOrEmpty(queryParams.StatusActive) || x.IsActive==true)
            )
        {
            AddInclude(x=>x.Campus);
            AddInclude(x=>x.Department);
            AddInclude(x=>x.Degree);
            AddInclude(x=>x.UserPhotos);
            AddOrderBy(x=>x.FirstName);
            ApplyPaging(queryParams.PageSize*(queryParams.PageIndex-1),queryParams.PageSize);
        }
    }
}
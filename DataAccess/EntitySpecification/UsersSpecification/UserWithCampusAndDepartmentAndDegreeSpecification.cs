using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.UsersSpecification
{
    public class UserWithCampusAndDepartmentAndDegreeSpecification:BaseSpecification<User>
    {
        public UserWithCampusAndDepartmentAndDegreeSpecification(string email)
        :base(x=>string.IsNullOrEmpty(email) || x.Email==email)
        {
            AddInclude(x=>x.Campus);
            AddInclude(x=>x.Department);
            AddInclude(x=>x.Degree);
            AddInclude(x=>x.UserPhotos);
            
        }
    }
}
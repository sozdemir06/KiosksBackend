using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.UsersSpecification
{
    public class UserWithPhotoByIdSpecification:BaseSpecification<User>
    {
         public UserWithPhotoByIdSpecification(int userId)
        :base(x=> x.Id==userId)
        {
            AddInclude(x=>x.Campus);
            AddInclude(x=>x.Department);
            AddInclude(x=>x.Degree);
            AddInclude(x=>x.UserPhotos);
            
        }
    }
}
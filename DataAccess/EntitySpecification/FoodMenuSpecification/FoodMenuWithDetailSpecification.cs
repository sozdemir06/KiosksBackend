using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.FoodMenuSpecification
{
    public class FoodMenuWithDetailSpecification : BaseSpecification<FoodMenu>
    {
        public FoodMenuWithDetailSpecification()
        {
            AddInclude(x => x.FoodMenuPhotos);
            AddInclude(x => x.FoodMenuSubScreens);
            AddInclude(x => x.User);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.User.Department);
        }
        public FoodMenuWithDetailSpecification(int foodMenuId) : base(x => x.Id == foodMenuId)
        {
            AddInclude(x => x.FoodMenuPhotos);
            AddInclude(x => x.FoodMenuSubScreens);
            AddInclude(x => x.User);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.User.Department);
        }
    }
}
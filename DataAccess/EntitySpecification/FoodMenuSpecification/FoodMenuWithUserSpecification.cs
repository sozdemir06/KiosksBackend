using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.FoodMenuSpecification
{
    public class FoodMenuWithUserSpecification:BaseSpecification<FoodMenu>
    {
        public FoodMenuWithUserSpecification(int foodMenuId):base(x=>x.Id==foodMenuId)
        {
            AddInclude(x=>x.User);
        }
    }
}
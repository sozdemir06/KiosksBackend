using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.FoodMenuSubScreenSpecification
{
    public class FoodMenuSubScreenWithSubScreenSpecification:BaseSpecification<FoodMenuSubscreen>
    {
        public FoodMenuSubScreenWithSubScreenSpecification(int foodMenuId):base(x=>x.FoodMenuId==foodMenuId)
        {
            AddInclude(x=>x.SubScreen);
        }
    }
}
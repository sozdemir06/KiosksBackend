using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.FoodMenuSubScreenSpecification
{
    public class FoodMenuSubScreenWithSubScreenForReturnSpecification:BaseSpecification<FoodMenuSubscreen>
    {
        public FoodMenuSubScreenWithSubScreenForReturnSpecification(int foodMenuSubSCreendId):base(x=>x.Id==foodMenuSubSCreendId)
        {
            AddInclude(x=>x.SubScreen);

        }
    }
}
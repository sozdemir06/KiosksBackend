using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.FoodMenuSpecification
{
    public class FoodMenuByUserIdSpecification:BaseSpecification<FoodMenu>
    {
        public FoodMenuByUserIdSpecification(FoodMenuParams queryParams,int userId)
        :base(x=>x.UserId==userId)
        {
            AddInclude(x=>x.User);
            AddInclude(x=>x.FoodMenuPhotos);
            AddOrderByDscending(x => x.Created);
            ApplyPaging(queryParams.PageSize * (queryParams.PageIndex - 1), queryParams.PageSize);
        }
    }
}
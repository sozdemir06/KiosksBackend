using System.Linq;
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.FoodMenuSpecification
{
    public class FoodMenuWithFilterForCountAsyncSpecification:BaseSpecification<FoodMenu>
    {
        public FoodMenuWithFilterForCountAsyncSpecification(FoodMenuParams queryParams)
         : base(x =>
             (
                 string.IsNullOrEmpty(queryParams.Search) ||
                 x.Content.ToLower().Contains(queryParams.Search) ||
                 x.User.FirstName.ToLower().Contains(queryParams.Search) ||
                 x.User.LastName.ToLower().Contains(queryParams.Search)

             ) &&
             (!queryParams.ScreenId.HasValue || x.FoodMenuSubScreens.Any(y => y.ScreenId == queryParams.ScreenId)) &&
             (!queryParams.SubScreenId.HasValue || x.FoodMenuSubScreens.Any(y => y.SubScreenId == queryParams.SubScreenId)) &&
             (!queryParams.Reject.HasValue || x.Reject == queryParams.Reject) &&
             (!queryParams.IsNew.HasValue || x.IsNew == queryParams.IsNew) &&
             (!queryParams.IsPublish.HasValue || x.IsPublish == queryParams.IsPublish)
        )
        {
            
        }
    }
}
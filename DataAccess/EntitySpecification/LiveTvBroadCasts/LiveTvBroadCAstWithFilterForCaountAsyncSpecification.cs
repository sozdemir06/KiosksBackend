using System.Linq;
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.LiveTvBroadCasts
{
    public class LiveTvBroadCAstWithFilterForCaountAsyncSpecification : BaseSpecification<LiveTvBroadCast>
    {

        public LiveTvBroadCAstWithFilterForCaountAsyncSpecification(LiveTvBroadCastParams queryParams)
         : base(x =>
             (
                 string.IsNullOrEmpty(queryParams.Search) ||
                 x.Header.ToLower().Contains(queryParams.Search) ||
                 x.ContentType.ToLower().Contains(queryParams.Search) ||
                 x.User.FirstName.ToLower().Contains(queryParams.Search) ||
                 x.User.LastName.ToLower().Contains(queryParams.Search)

             ) &&
             (!queryParams.ScreenId.HasValue || x.LiveTvBroadCastSubScreens.Any(y => y.ScreenId == queryParams.ScreenId)) &&
             (!queryParams.SubScreenId.HasValue || x.LiveTvBroadCastSubScreens.Any(y => y.Id == queryParams.SubScreenId)) &&
             (!queryParams.Reject.HasValue || x.Reject == queryParams.Reject) &&
             (!queryParams.IsNew.HasValue || x.IsNew == queryParams.IsNew) &&
             (!queryParams.IsPublish.HasValue || x.IsPublish == queryParams.IsPublish)
        )
        {

        }
    }
}
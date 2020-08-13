using System.Linq;
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.NewsSpecification
{
    public class NewsWithFilterForCountAsyncSpecificaiton : BaseSpecification<News>
    {
        public NewsWithFilterForCountAsyncSpecificaiton(NewsParams queryParams)
        : base(x =>
             (
                 string.IsNullOrEmpty(queryParams.Search) ||
                 x.Header.ToLower().Contains(queryParams.Search) ||
                 x.Content.ToLower().Contains(queryParams.Search) ||
                 x.ContentType.ToLower().Contains(queryParams.Search) ||
                 x.User.FirstName.ToLower().Contains(queryParams.Search) ||
                 x.User.LastName.ToLower().Contains(queryParams.Search)

             ) &&
             (!queryParams.ScreenId.HasValue || x.NewsSubScreens.Any(y => y.ScreenId == queryParams.ScreenId)) &&
             (!queryParams.SubScreenId.HasValue || x.NewsSubScreens.Any(y => y.Id == queryParams.SubScreenId)) &&
             (!queryParams.Reject.HasValue || x.Reject == queryParams.Reject) &&
             (!queryParams.IsNew.HasValue || x.IsNew == queryParams.IsNew) &&
             (!queryParams.IsPublish.HasValue || x.IsPublish == queryParams.IsPublish)
        )
        {

        }
    }
}
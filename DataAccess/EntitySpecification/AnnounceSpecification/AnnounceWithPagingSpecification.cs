using System.Linq;
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.AnnounceSpecification
{
    public class AnnounceWithPagingSpecification : BaseSpecification<Announce>
    {
        public AnnounceWithPagingSpecification(AnnounceParams queryParams)
         : base(x =>
             (
                 string.IsNullOrEmpty(queryParams.Search) ||
                 x.Header.ToLower().Contains(queryParams.Search) ||
                 x.Content.ToLower().Contains(queryParams.Search) ||
                 x.ContentType.ToLower().Contains(queryParams.Search) ||
                 x.User.FirstName.ToLower().Contains(queryParams.Search) ||
                 x.User.LastName.ToLower().Contains(queryParams.Search)

             ) &&
             (!queryParams.ScreenId.HasValue || x.AnnounceSubScreens.Any(y => y.ScreenId == queryParams.ScreenId)) &&
             (!queryParams.SubScreenId.HasValue || x.AnnounceSubScreens.Any(y => y.SubScreenId == queryParams.SubScreenId)) &&
             (!queryParams.Reject.HasValue || x.Reject == queryParams.Reject) &&
             (!queryParams.IsNew.HasValue || x.IsNew == queryParams.IsNew) &&
             (!queryParams.IsPublish.HasValue || x.IsPublish == queryParams.IsPublish)
        )
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.AnnouncePhotos);
            AddInclude(x => x.AnnounceSubScreens);
            AddOrderByDscending(x => x.IsNew);
            ApplyPaging(queryParams.PageSize * (queryParams.PageIndex - 1), queryParams.PageSize);
        }
    }
}
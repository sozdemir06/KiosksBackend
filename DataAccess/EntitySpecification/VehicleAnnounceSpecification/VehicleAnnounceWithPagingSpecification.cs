using System.Linq;
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.VehicleAnnounceSpecification
{
    public class VehicleAnnounceWithPagingSpecification : BaseSpecification<VehicleAnnounce>
    {
        public VehicleAnnounceWithPagingSpecification(VehicleAnnounceParams queryParams)
         : base(x =>
             (
                 string.IsNullOrEmpty(queryParams.Search) ||
                 x.Header.ToLower().Contains(queryParams.Search) ||
                 x.Description.ToLower().Contains(queryParams.Search) ||
                 x.User.FirstName.ToLower().Contains(queryParams.Search) ||
                 x.User.LastName.ToLower().Contains(queryParams.Search)

             ) &&
             (!queryParams.ScreenId.HasValue || x.VehicleAnnounceSubScreens.Any(y => y.ScreenId == queryParams.ScreenId)) &&
             (!queryParams.SubScreenId.HasValue || x.VehicleAnnounceSubScreens.Any(y => y.Id == queryParams.SubScreenId)) &&
             (!queryParams.Reject.HasValue || x.Reject == queryParams.Reject) &&
             (!queryParams.IsNew.HasValue || x.IsNew == queryParams.IsNew) &&
             (!queryParams.IsPublish.HasValue || x.IsPublish == queryParams.IsPublish)
        )
        {
            AddInclude(x=>x.User);
            AddOrderByDscending(x => x.IsNew);
            ApplyPaging(queryParams.PageSize * (queryParams.PageIndex - 1), queryParams.PageSize);
        }

        public VehicleAnnounceWithPagingSpecification(int announceId):base(x=>x.Id==announceId)
        {
            AddInclude(x=>x.User);
        }
    }
}
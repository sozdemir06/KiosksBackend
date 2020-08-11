using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;
using System.Linq;

namespace DataAccess.EntitySpecification.HomeAnnounceSpecification
{
    public class HomeAnnounceWithPhotoAndUserSpecification:BaseSpecification<HomeAnnounce>
    {
        public HomeAnnounceWithPhotoAndUserSpecification(HomeAnnounceParams queryParams)
        :base(x=>
            (
                string.IsNullOrEmpty(queryParams.Search) || 
                x.Header.ToLower().Contains(queryParams.Search) ||
                x.Description.ToLower().Contains(queryParams.Search) ||
                x.User.FirstName.ToLower().Contains(queryParams.Search) ||
                x.User.LastName.ToLower().Contains(queryParams.Search) 
                
            ) &&
            (!queryParams.ScreenId.HasValue || x.HomeAnnounceSubScreens.Any(y=>y.ScreenId==queryParams.ScreenId)) &&
            (!queryParams.SubScreenId.HasValue || x.HomeAnnounceSubScreens.Any(y=>y.SubScreenId==queryParams.SubScreenId)) &&
            (!queryParams.Reject.HasValue || x.Reject==queryParams.Reject) &&
            (!queryParams.IsNew.HasValue || x.IsNew==queryParams.IsNew) &&
            (!queryParams.IsPublish.HasValue || x.IsPublish==queryParams.IsPublish)
        )
        {
            AddInclude(x=>x.User);
            AddOrderByDscending(x=>x.IsNew);
            ApplyPaging(queryParams.PageSize*(queryParams.PageIndex-1),queryParams.PageSize);
        }

        public HomeAnnounceWithPhotoAndUserSpecification(int announceId):base(x=>x.Id==announceId)
        {
            AddInclude(x=>x.HomeAnnouncePhotos);
            AddInclude(x=>x.User);
            AddInclude(x=>x.User.Department);
            AddInclude(x=>x.User.Campus);
            AddInclude(x=>x.User.Degree);
            AddInclude(x=>x.NumberOfRoom);
            AddInclude(x=>x.Heatingtype);
            AddInclude(x=>x.FlatOfHome);
            AddInclude(x=>x.BuildingAge);
        }
    }
}
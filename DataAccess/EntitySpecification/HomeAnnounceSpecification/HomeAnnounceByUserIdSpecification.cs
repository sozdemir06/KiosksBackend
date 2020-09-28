using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.HomeAnnounceSpecification
{
    public class HomeAnnounceByUserIdSpecification : BaseSpecification<HomeAnnounce>
    {
        public HomeAnnounceByUserIdSpecification(HomeAnnounceParams queryParams, int userId)
        : base(x => x.UserId == userId)
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.NumberOfRoom);
            AddInclude(x => x.Heatingtype);
            AddInclude(x => x.BuildingAge);
            AddInclude(x => x.FlatOfHome);
            AddInclude(x => x.HomeAnnouncePhotos);
            AddOrderByDscending(x => x.Created);
            ApplyPaging(queryParams.PageSize * (queryParams.PageIndex - 1), queryParams.PageSize);
        }

        public HomeAnnounceByUserIdSpecification(int userId) : base(x => x.UserId == userId)
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.NumberOfRoom);
            AddInclude(x => x.Heatingtype);
            AddInclude(x => x.BuildingAge);
            AddInclude(x => x.FlatOfHome);
            AddInclude(x => x.HomeAnnouncePhotos);
        }

        public HomeAnnounceByUserIdSpecification(int userId, int announceId) : base(x => x.UserId == userId && x.Id == announceId)
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.NumberOfRoom);
            AddInclude(x => x.Heatingtype);
            AddInclude(x => x.BuildingAge);
            AddInclude(x => x.FlatOfHome);
            AddInclude(x => x.HomeAnnouncePhotos);
        }
    }
}
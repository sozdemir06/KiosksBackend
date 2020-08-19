using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.HomeAnnounceSpecification
{
    public class HomeAnnounceDetailSpecification:BaseSpecification<HomeAnnounce>
    {
        public HomeAnnounceDetailSpecification()
        {
            AddInclude(x=>x.HomeAnnouncePhotos);
            AddInclude(x=>x.HomeAnnounceSubScreens);
            AddInclude(x=>x.User.Department);
            AddInclude(x=>x.User.Campus);
            AddInclude(x=>x.User.Degree);
            AddInclude(x=>x.NumberOfRoom);
            AddInclude(x=>x.Heatingtype);
            AddInclude(x=>x.FlatOfHome);
            AddInclude(x=>x.BuildingAge);
        }
        public HomeAnnounceDetailSpecification(int homeAnnounceId)
        :base(x=>x.Id==homeAnnounceId)
        {
            AddInclude(x=>x.HomeAnnouncePhotos);
            AddInclude(x=>x.HomeAnnounceSubScreens);
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
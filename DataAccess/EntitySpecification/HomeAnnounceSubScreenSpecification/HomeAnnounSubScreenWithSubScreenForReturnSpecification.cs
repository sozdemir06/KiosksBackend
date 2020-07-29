using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.HomeAnnounceSubScreenSpecification
{
    public class HomeAnnounSubScreenWithSubScreenForReturnSpecification:BaseSpecification<HomeAnnounceSubScreen>
    {
        public HomeAnnounSubScreenWithSubScreenForReturnSpecification(int homeAnnounceSubScreenId)
        :base(x=>x.Id==homeAnnounceSubScreenId)
        {
            AddInclude(x=>x.SubScreen);
        }
    }
}
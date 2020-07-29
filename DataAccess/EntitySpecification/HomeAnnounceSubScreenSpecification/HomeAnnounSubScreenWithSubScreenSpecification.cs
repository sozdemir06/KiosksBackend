using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.HomeAnnounceSubScreenSpecification
{
    public class HomeAnnounSubScreenWithSubScreenSpecification:BaseSpecification<HomeAnnounceSubScreen>
    {
        public HomeAnnounSubScreenWithSubScreenSpecification(int AnnounceId)
        :base(x=>x.HomeAnnounceId==AnnounceId)
        {
            AddInclude(x=>x.SubScreen);
        }
    }
}
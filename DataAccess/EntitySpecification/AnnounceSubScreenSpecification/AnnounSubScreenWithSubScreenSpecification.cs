using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.AnnounceSubScreenSpecification
{
    public class AnnounSubScreenWithSubScreenSpecification:BaseSpecification<AnnounceSubScreen>
    {
        public AnnounSubScreenWithSubScreenSpecification(int announceId):base(x=>x.AnnounceId==announceId)
        {
            AddInclude(x=>x.SubScreen);
        }
    }
}
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.AnnounceSubScreenSpecification
{
    public class AnnounSubScreenWithSubScreenForReturnSpecification:BaseSpecification<AnnounceSubScreen>
    {
        public AnnounSubScreenWithSubScreenForReturnSpecification(int announceSubsCreenId)
        :base(x=>x.Id==announceSubsCreenId)
        {
            AddInclude(x=>x.SubScreen);
        }
    }
}
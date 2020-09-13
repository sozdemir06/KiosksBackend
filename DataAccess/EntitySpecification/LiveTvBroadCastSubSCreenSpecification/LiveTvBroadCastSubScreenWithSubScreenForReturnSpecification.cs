using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.LiveTvBroadCastSubSCreenSpecification
{
    public class LiveTvBroadCastSubScreenWithSubScreenForReturnSpecification:BaseSpecification<LiveTvBroadCastSubScreen>
    {
         public LiveTvBroadCastSubScreenWithSubScreenForReturnSpecification(int liveTvBroadCastSubScreenId)
        :base(x=>x.Id==liveTvBroadCastSubScreenId)
        {
            AddInclude(x=>x.SubScreen);
        }
    }
}
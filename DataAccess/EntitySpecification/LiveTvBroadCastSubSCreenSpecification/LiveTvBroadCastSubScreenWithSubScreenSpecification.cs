using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.LiveTvBroadCastSubSCreenSpecification
{
    public class LiveTvBroadCastSubScreenWithSubScreenSpecification:BaseSpecification<LiveTvBroadCastSubScreen>
    {
        public LiveTvBroadCastSubScreenWithSubScreenSpecification(int liveTvBroadCastId):base(x=>x.LiveTvBroadCastId==liveTvBroadCastId)
        {
            AddInclude(x=>x.SubScreen);
        }
    }
}
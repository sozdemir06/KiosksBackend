using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.LiveTvBroadCasts
{
    public class LiveTvBroadCastWithUserSpecification:BaseSpecification<LiveTvBroadCast>
    {
        public LiveTvBroadCastWithUserSpecification(int liveTvBroadCastId):base(x=>x.Id==liveTvBroadCastId)
        {
            AddInclude(x=>x.User);
        }
    }
}
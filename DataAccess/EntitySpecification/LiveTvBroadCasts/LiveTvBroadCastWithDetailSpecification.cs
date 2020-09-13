using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.LiveTvBroadCasts
{
    public class LiveTvBroadCastWithDetailSpecification:BaseSpecification<LiveTvBroadCast>
    {
        public LiveTvBroadCastWithDetailSpecification()
        {
            AddInclude(x => x.LiveTvBroadCastSubScreens);
            AddInclude(x => x.User);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.User.Department);
        }

         public LiveTvBroadCastWithDetailSpecification(int liveTvBroadCastId) : base(x => x.Id == liveTvBroadCastId)
        {
            AddInclude(x => x.LiveTvBroadCastSubScreens);
            AddInclude(x => x.User);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.User.Department);
        }
    }
}
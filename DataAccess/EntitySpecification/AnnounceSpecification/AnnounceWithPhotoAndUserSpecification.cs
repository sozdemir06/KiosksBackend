using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.AnnounceSpecification
{
    public class AnnounceWithUserSpecification : BaseSpecification<Announce>
    {
        public AnnounceWithUserSpecification(int announceId) : base(x => x.Id == announceId)
        {

            AddInclude(x => x.User);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.AnnouncePhotos);
            AddInclude(x => x.AnnounceSubScreens);
        }
    }
}
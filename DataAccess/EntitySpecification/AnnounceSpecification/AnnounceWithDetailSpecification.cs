using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.AnnounceSpecification
{
    public class AnnounceWithDetailSpecification:BaseSpecification<Announce>
    {
        public AnnounceWithDetailSpecification(int announceId):base(x=>x.Id==announceId)
        {
            AddInclude(x=>x.AnnouncePhotos);
            AddInclude(x=>x.AnnounceSubScreens);
            AddInclude(x=>x.User);
            AddInclude(x=>x.User.Campus);
            AddInclude(x=>x.User.Degree);
            AddInclude(x=>x.User.Department);
        }
    }
}
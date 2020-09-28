using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.AnnounceSpecification
{
    public class AnnounceByUserIdSpecification : BaseSpecification<Announce>
    {
        public AnnounceByUserIdSpecification(AnnounceParams queryParams, int userId)
        : base(x => x.UserId == userId)
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.AnnouncePhotos);
            AddOrderByDscending(x => x.Created);
            ApplyPaging(queryParams.PageSize * (queryParams.PageIndex - 1), queryParams.PageSize);
        }

        public AnnounceByUserIdSpecification(int userId)
        : base(
            x => x.UserId == userId
        )
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.AnnouncePhotos);
        }

        public AnnounceByUserIdSpecification(int userId, int announceId) : base(x => x.UserId == userId && x.Id == announceId)
        {
            AddInclude(x => x.User);
            AddInclude(x => x.User.Department);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.AnnouncePhotos);
        }
    }
}
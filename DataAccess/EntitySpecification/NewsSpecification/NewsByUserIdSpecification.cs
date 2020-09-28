using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.NewsSpecification
{
    public class NewsByUserIdSpecification:BaseSpecification<News>
    {
        public NewsByUserIdSpecification(NewsParams queryParams,int userId)
        :base(x=>x.UserId==userId)
        {
            AddInclude(x => x.User);
            AddInclude(x=>x.NewsPhotos);
            AddOrderByDscending(x => x.Created);
            ApplyPaging(queryParams.PageSize * (queryParams.PageIndex - 1), queryParams.PageSize);
        }
    }
}
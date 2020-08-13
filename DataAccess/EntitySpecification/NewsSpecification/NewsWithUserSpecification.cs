using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.NewsSpecification
{
    public class NewsWithUserSpecification:BaseSpecification<News>
    {
        public NewsWithUserSpecification(int newsId):base(x=>x.Id==newsId)
        {
            AddInclude(x=>x.User);
        }
    }
}
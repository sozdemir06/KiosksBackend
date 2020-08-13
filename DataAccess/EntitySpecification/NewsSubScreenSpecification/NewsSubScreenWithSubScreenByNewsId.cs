using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.NewsSubScreenSpecification
{
    public class NewsSubScreenWithSubScreenByNewsId : BaseSpecification<NewsSubScreen>
    {
        public NewsSubScreenWithSubScreenByNewsId(int newsId) : base(x => x.NewsId == newsId)
        {
            AddInclude(x => x.SubScreen);
        }
    }
}
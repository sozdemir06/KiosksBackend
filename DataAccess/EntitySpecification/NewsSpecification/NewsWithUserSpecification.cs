using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.NewsSpecification
{
    public class NewsWithUserSpecification : BaseSpecification<News>
    {
        public NewsWithUserSpecification(int newsId) : base(x => x.Id == newsId)
        {
            AddInclude(x => x.NewsPhotos);
            AddInclude(x => x.NewsSubScreens);
            AddInclude(x => x.User);
            AddInclude(x => x.User.Campus);
            AddInclude(x => x.User.Degree);
            AddInclude(x => x.User.Department);
        }
    }
}
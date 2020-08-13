using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.NewsSubScreenSpecification
{
    public class NewsSubScreenWithSubScreenForReturnSpecification:BaseSpecification<NewsSubScreen>
    {
        public NewsSubScreenWithSubScreenForReturnSpecification(int newsSubScreenId)
        :base(x=>x.Id==newsSubScreenId)
        {
            AddInclude(x=>x.SubScreen);
        }
       
    }
}
using Core.DataAccess.Specifications;
using Entities.Concrete;

namespace DataAccess.EntitySpecification.ScreenSpecification
{
    public class ScreenWithSubScreenSpecification:BaseSpecification<Screen>
    {
        public ScreenWithSubScreenSpecification(int screenId):base(x=>x.Id==screenId)
        {
            AddInclude(x=>x.SubScreens);
            AddOrderBy(x=>x.Name);
        }
    }
}
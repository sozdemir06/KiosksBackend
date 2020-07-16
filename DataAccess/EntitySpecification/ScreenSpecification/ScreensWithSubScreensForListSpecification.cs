using Core.DataAccess.Specifications;
using Core.Entities.Concrete;

namespace DataAccess.EntitySpecification.ScreenSpecification
{
    public class ScreensWithSubScreensForListSpecification:BaseSpecification<Screen>
    {
       public ScreensWithSubScreensForListSpecification()
       {
           AddInclude(x=>x.SubScreens);
           AddOrderBy(x=>x.Name);
       } 
    }
}
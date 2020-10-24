using Core.DataAccess.Specifications;
using Core.Entities.Concrete;


namespace DataAccess.EntitySpecification.ScreenSpecification
{
    public class ScreenWithSubScreenSpecification : BaseSpecification<Screen>
    {
        public ScreenWithSubScreenSpecification(int screenId) : base(x => x.Id == screenId)
        {
            AddInclude(x => x.SubScreens);
            AddInclude(x => x.ScreenHeaders);
            AddInclude(x => x.ScreenHeaderPhotos);
            AddInclude(x => x.ScreenFooters);
            AddOrderBy(x => x.Name);
        }

        public ScreenWithSubScreenSpecification()
        {
            AddInclude(x => x.SubScreens);
            AddInclude(x => x.ScreenHeaderPhotos);
            AddInclude(x => x.ScreenFooters);
            AddInclude(x => x.ScreenHeaders);
            AddOrderBy(x => x.Name);
        }
    }
}
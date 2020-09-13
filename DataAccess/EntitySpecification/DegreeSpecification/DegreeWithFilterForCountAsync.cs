using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.DegreeSpecification
{
    public class DegreeWithFilterForCountAsync : BaseSpecification<Degree>
    {
        public DegreeWithFilterForCountAsync(DegreeParams queryParams)
         : base(x =>
             (string.IsNullOrEmpty(queryParams.Search)
             || x.Name.ToLower().Contains(queryParams.Search)
             )

        )
        {

        }
    }
}
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using Core.QueryParams;

namespace DataAccess.EntitySpecification.DegreeSpecification
{
    public class DegreeForPagingSpecification:BaseSpecification<Degree>
    {
        public DegreeForPagingSpecification(DegreeParams queryParams)
         :base(x=>
            (string.IsNullOrEmpty(queryParams.Search) 
            || x.Name.ToLower().Contains(queryParams.Search)
            )
            
        )
        {
            ApplyPaging(queryParams.PageSize*(queryParams.PageIndex-1),queryParams.PageSize);
        }
    }
}
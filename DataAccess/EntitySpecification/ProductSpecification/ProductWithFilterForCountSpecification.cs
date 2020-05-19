using Core.DataAccess.Specifications;
using Core.QueryParams;
using Entities.Concrete;

namespace DataAccess.EntitySpecification.ProductSpecification
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductQueryParams queryParams)
            :base(x=>
                (!queryParams.CategoryId.HasValue || x.CategoryId==queryParams.CategoryId)
            )
        {
        }
    }
}
using Core.DataAccess.Specifications;
using Core.QueryParams;
using Entities.Concrete;

namespace DataAccess.EntitySpecification.ProductSpecification
{
    public class ProductWithCategorySpecification : BaseSpecification<Product>
    {
        public ProductWithCategorySpecification(ProductQueryParams queryParams)
            :base(x=>
                (!queryParams.CategoryId.HasValue || x.CategoryId==queryParams.CategoryId)
            )
        {
            AddInclude(x=>x.Category);
            AddOrderBy(x=>x.ProductName);
            ApplyPaging(queryParams.PageSize*(queryParams.PageIndex-1),queryParams.PageSize);

            if(!string.IsNullOrEmpty(queryParams.Sort))
            {
                switch(queryParams.Sort)
                {
                    case "priceAsc":
                    AddOrderBy(x=>x.Price);
                    break;
                    case "priceDesc":
                    AddOrderByDscending(x=>x.Price);
                    break;
                    default:
                    AddOrderBy(n=>n.ProductName);
                    break;
                }
            }
        }

        public ProductWithCategorySpecification(int id) 
                : base(x=>x.ProductId==id)
        {
            AddInclude(x=>x.Category);
        }
    }
}
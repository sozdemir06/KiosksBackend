using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Helpers;
using Core.QueryParams;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<Pagination<ProductForListDto>> GetProductListAsync(ProductQueryParams queryParams);
        Task<Product> GetProductByIdAsync(int productId);
        Task<List<Product>> GetProductByCategoryIdAsync(int categoryId);
        Task<ProductForListDto> Create(Product product);
        void Delete(Product product);
        Task<ProductForListDto> UpdateExist(Product product);
    }
}
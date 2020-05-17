using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<List<Product>> GetProductListAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<List<Product>> GetProductByCategoryIdAsync(int categoryId);
        void Add(Product product);
        void Delete(Product product);
        void Update(Product product);
    }
}
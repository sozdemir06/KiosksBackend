using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq;
namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal productDal;
        public ProductManager(IProductDal productDal)
        {
            this.productDal = productDal;

        }

        public void Add(Product product)
        {

            productDal.Add(product);
        }

        public void Delete(Product product)
        {
            productDal.Delete(product);
        }

        public async Task<List<Product>> GetProductByCategoryIdAsync(int categoryId)
        {
            var products = await productDal.GetListAsync(x => x.CategoryId == categoryId);
            if (products == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Product=Messages.ProductNotFound });
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await productDal.GetAsync(x => x.ProductId == productId);
            if (product == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Messages.ProductNotFound });
            }

            return product;
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            var products = await productDal.GetListAsync();
            if (products.Count == 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Product=Messages.ProductNotFound });
            }

            return products;
        }

        public void Update(Product product)
        {
            productDal.Update(product);
            var result=productDal.SaveChanges();
            if(!result)
            {
                 throw new RestException(HttpStatusCode.BadRequest, new { product=Messages.UpdateProblem });
            }

        }
    }
}
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq;
using Core.QueryParams;
using DataAccess.EntitySpecification.ProductSpecification;
using Business.Helpers;
using AutoMapper;
using Entities.Dtos;
using Business.ValidaitonRules.FluentValidation;
using FluentValidation;
using Core.Aspects.AutoFac.Validation;
using Core.Aspects.AutoFac.Caching;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal productDal;
        private readonly IMapper mapper;
        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.productDal = productDal;

        }

        [ValidationAspect(typeof(ProductValidatior))]
        // [CacheRemoveAspect("IProductService.Get")]
        public async Task<ProductForListDto> Add(Product product)
        {


            var addedProduct = await productDal.Add(product);
            

            return mapper.Map<Product, ProductForListDto>(addedProduct);

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
                throw new RestException(HttpStatusCode.BadRequest, new { Product = Messages.ProductNotFound });
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

        [CacheAspect(1)]
        public async Task<Pagination<ProductForListDto>> GetProductListAsync(ProductQueryParams queryParams)
        {
            var spec = new ProductWithCategorySpecification(queryParams);
            var countSpec = new ProductWithFilterForCountSpecification(queryParams);
            var totalItems = await productDal.CountAsync(countSpec);

            var products = await productDal.ListEntityWithSpecAsync(spec);
            if (products == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Product = Messages.ProductNotFound });
            }

            var data = mapper.Map<List<Product>, List<ProductForListDto>>(products);

            return new Pagination<ProductForListDto>
            (
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItems,
                data

            );
        }

        [CacheRemoveAspect("IProductService.Get")]
        public async Task<ProductForListDto> Update(Product product)
        {
            var updatedProduct = await productDal.Update(product);

            return mapper.Map<Product, ProductForListDto>(updatedProduct);

        }

    }
}
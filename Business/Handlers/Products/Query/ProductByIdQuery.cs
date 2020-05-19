using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.Constants;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.ProductSpecification;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;

namespace Business.Handlers.Products.Query
{
    public class ProductByIdQuery : IRequest<ProductForListDto>
    {
        public int ProductId { get; set; }


        public class ProductByIdHandler : IRequestHandler<ProductByIdQuery, ProductForListDto>
        {
            private readonly IProductDal productDal;
            private readonly IMapper mapper;
            public ProductByIdHandler(IProductDal productDal, IMapper mapper)
            {
                this.mapper = mapper;
                this.productDal = productDal;

            }
            public async Task<ProductForListDto> Handle(ProductByIdQuery request, CancellationToken cancellationToken)
            {
                var spec = new ProductWithCategorySpecification(request.ProductId);
                var product = await  productDal.GetEntityWithSpecAsync(spec);
                if (product == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { ProductNotFound = Messages.ProductNotFound });
                }

                var productForReturn = mapper.Map<Product,ProductForListDto>(product);
                return productForReturn;

            }
        }

    }
}
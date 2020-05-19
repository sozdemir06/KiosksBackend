using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.Constants;
using Business.Helpers;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.EntitySpecification.ProductSpecification;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;

namespace Business.Handlers.Products.Query
{
    public class ProductListQuery : IRequest<Pagination<ProductForListDto>>
    {
       private const int MaxPageSize=50;
        public int PageIndex { get; set; }=1;
        private int _pageSize=6;

        public int PageSize 
        { 
            get=>_pageSize;
            set=>_pageSize=(value>MaxPageSize)?MaxPageSize:value; 
            
        }
        public string Sort { get; set; }

        public int? CategoryId { get; set; }

        public ProductListQuery(string sort,int pageSize,int pageIndex,int? categoryId)
        {
            Sort = sort;
            PageSize=pageSize;
            PageIndex=pageIndex;
            CategoryId=categoryId;
        }

        



        public class ProductListQueryHandler : IRequestHandler<ProductListQuery, Pagination<ProductForListDto>>
        {
            private readonly IProductDal productDal;
            private readonly IMapper mapper;
            public ProductListQueryHandler(IProductDal productDal, IMapper mapper)
            {
                this.mapper = mapper;
                this.productDal = productDal;
            }

            public Task<Pagination<ProductForListDto>> Handle(ProductListQuery request, CancellationToken cancellationToken)
            {
                // var spec = new ProductWithCategorySpecification(request.Sort, request.CategoryId, request.PageIndex, request.PageSize);
                // var countSpec = new ProductWithFilterForCountSpecification(request.Sort, request.CategoryId, request.PageIndex, request.PageSize);
                // var totalItems = await productDal.CountAsync(countSpec);
                // var products = await productDal.ListEntityWithSpecAsync(spec);
                // if (products == null)
                // {
                //     throw new RestException(HttpStatusCode.BadRequest, new { ProductNotFound = Messages.ProductNotFound });
                // }

                // var data = mapper.Map<List<Product>, List<ProductForListDto>>(products);

                // return new Pagination<ProductForListDto>
                // (
                //     request.PageIndex,
                //     request.PageSize,
                //     totalItems,
                //     data

                // );

                throw new NotImplementedException();



            }
        }
    }
}
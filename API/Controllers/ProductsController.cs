using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Handlers.Products.Query;
using Business.Helpers;
using Core.QueryParams;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMediator mediator;
        public ProductsController(IProductService productService, IMediator mediator)
        {
            this.mediator = mediator;
            this.productService = productService;
        }


        [HttpGet("getall")]
        public async Task<ActionResult<Pagination<ProductForListDto>>> GetList([FromQuery]ProductQueryParams queryParams)
        {
            return await productService.GetProductListAsync(queryParams);
        }

        [HttpGet("getlistbycategory")]
        public async Task<ActionResult<List<Product>>> GetListByCategory(int categoryId)
        {
            return await productService.GetProductByCategoryIdAsync(categoryId);
        }

        [HttpGet("getbyid/{productId}")]
        public async Task<ActionResult<ProductForListDto>> GetById(int productId)
        {
            return await mediator.Send(new ProductByIdQuery{ProductId=productId});
        }
    }
}
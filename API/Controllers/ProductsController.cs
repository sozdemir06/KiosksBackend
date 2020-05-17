using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }


        [HttpGet("getall")]
        public async Task<ActionResult<List<Product>>> GetList()
        {
            return await productService.GetProductListAsync();
        }

        [HttpGet("getlistbycategory")]
        public async Task<ActionResult<List<Product>>> GetListByCategory(int categoryId)
        {
            return await productService.GetProductByCategoryIdAsync(categoryId);
        }
    }
}
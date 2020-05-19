using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Handlers.Products
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product,ProductForListDto>();
        }
        
    }
}
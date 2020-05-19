using Entities.Concrete;

namespace Entities.Dtos
{
    public class ProductForListDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal Price { get; set; }
        public CategoryForListDto Category { get; set; }
    }
}
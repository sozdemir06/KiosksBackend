

using Core.Entities;

namespace Entities.Concrete
{
    public class Product:IEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }

        //Navigation Property
        public Category Category { get; set; }

    }
}
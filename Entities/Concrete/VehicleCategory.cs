using Core.Entities;

namespace Entities.Concrete
{
    public class VehicleCategory:IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
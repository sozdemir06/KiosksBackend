using Core.Entities;

namespace Entities.Concrete
{
    public class VehicleFuelType:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
using Core.Entities;

namespace Core.Entities.Concrete
{
    public class VehicleGearType:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
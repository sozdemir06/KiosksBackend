using Core.Entities;

namespace Core.Entities.Concrete
{
    public class VehicleEngineSize:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
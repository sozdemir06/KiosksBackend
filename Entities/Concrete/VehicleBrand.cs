using Core.Entities;

namespace Entities.Concrete
{
    public class VehicleBrand:IEntity
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int VehicleCategoryId { get; set; }
        public VehicleCategory VehicleCategories { get; set; }
    }
}
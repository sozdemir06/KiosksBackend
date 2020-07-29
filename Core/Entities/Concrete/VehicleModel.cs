using System.Collections.Generic;
using Core.Entities;

namespace Core.Entities.Concrete
{
    public class VehicleModel:IEntity
    {
        public int Id { get; set; }
        public string VehicleModelName { get; set; }
        public VehicleBrand VehicleBrands { get; set; }
        public int VehicleBrandId { get; set; }
        public VehicleCategory VehicleCategories { get; set; }
        public int VehicleCategoryId { get; set; }
         public ICollection<VehicleAnnounce> VehicleAnnounces { get; set; }

        
        
    }
}
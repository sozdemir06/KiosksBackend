using System.Collections.Generic;
using Core.Entities;

namespace Core.Entities.Concrete
{
    public class VehicleCategory:IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<VehicleBrand> VehicleBrands { get; set; }
        public ICollection<VehicleModel> VehicleModels { get; set; }
        public ICollection<VehicleAnnounce> VehicleAnnounces { get; set; }
    }
}
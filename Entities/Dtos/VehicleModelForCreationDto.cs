using Core.Entities;

namespace Entities.Dtos
{
    public class VehicleModelForCreationDto : IDto
    {
        public int Id { get; set; }
        public string VehicleModelName { get; set; }
        public int VehicleBrandId { get; set; }
        public int VehicleCategoryId { get; set; }
    }
}
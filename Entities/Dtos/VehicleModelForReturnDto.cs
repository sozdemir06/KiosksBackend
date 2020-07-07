using Core.Entities;

namespace Entities.Dtos
{
    public class VehicleModelForReturnDto : IDto
    {
        public int Id { get; set; }
        public string VehicleModelName { get; set; }
        public int VehicleBrandId { get; set; }
        public string VehicleBrandsBrandName { get; set; }
        public int VehicleCategoryId { get; set; }
        public string VehicleCategoriesCategoryName { get; set; }

    }
}
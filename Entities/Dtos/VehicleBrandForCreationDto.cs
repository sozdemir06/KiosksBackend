using Core.Entities;

namespace Entities.Dtos
{
    public class VehicleBrandForCreationDto : IDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int VehicleCategoryId { get; set; }
    }
}
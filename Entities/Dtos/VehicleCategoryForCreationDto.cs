using Core.Entities;

namespace Entities.Dtos
{
    public class VehicleCategoryForCreationDto : IDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
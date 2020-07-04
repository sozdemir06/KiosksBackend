using Core.Entities;

namespace Entities.Dtos
{
    public class VehicleCategoryForReturnDto : IDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
using Core.Entities;

namespace Entities.Dtos
{
    public class BuildingAgeForReturnDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
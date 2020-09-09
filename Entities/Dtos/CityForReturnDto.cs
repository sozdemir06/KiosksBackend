using Core.Entities;

namespace Entities.Dtos
{
    public class CityForReturnDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public int CityId { get; set; }
    }
}
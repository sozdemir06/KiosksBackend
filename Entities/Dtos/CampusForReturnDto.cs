using Core.Entities;

namespace Entities.Dtos
{
    public class CampusForReturnDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
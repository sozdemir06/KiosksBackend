using Core.Entities;

namespace Entities.Dtos
{
    public class FlatOfHomeForReturnDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
using Core.Entities;

namespace Entities.Dtos
{
    public class FlatOfHomeForCreationDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
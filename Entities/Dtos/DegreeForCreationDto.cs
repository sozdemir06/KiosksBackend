using Core.Entities;

namespace Entities.Dtos
{
    public class DegreeForCreationDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
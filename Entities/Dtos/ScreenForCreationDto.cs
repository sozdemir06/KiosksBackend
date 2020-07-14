using Core.Entities;

namespace Entities.Dtos
{
    public class ScreenForCreationDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public bool IsFull { get; set; }
        
    }
}
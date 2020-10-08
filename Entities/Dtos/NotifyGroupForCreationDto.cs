using Core.Entities;

namespace Entities.Dtos
{
    public class NotifyGroupForCreationDto:IDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        
    }
}
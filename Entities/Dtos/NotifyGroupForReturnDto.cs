using Core.Entities;

namespace Entities.Dtos
{
    public class NotifyGroupForReturnDto:IDto
    {
         public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        
    }
}
using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    public class NotifyGroup:IEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public ICollection<UserNotifyGroup> UserNotifyGroups { get; set; }
        
    }
}
using Core.Entities;

namespace Entities.Dtos
{
    public class UserNotifyGroupForReturnDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NotifyGroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
    }
}
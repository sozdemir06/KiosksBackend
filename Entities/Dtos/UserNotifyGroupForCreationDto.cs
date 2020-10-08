using Core.Entities;

namespace Entities.Dtos
{
    public class UserNotifyGroupForCreationDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NotifyGroupId { get; set; }
    }
}
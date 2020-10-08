namespace Core.Entities.Concrete
{
    public class UserNotifyGroup:IEntity
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public NotifyGroup NotifyGroup { get; set; }
        public int NotifyGroupId { get; set; }
    }
}
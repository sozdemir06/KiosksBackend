namespace Core.Entities.Concrete
{
    public class OnlineUser:IEntity
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
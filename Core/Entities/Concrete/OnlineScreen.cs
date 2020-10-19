namespace Core.Entities.Concrete
{
    public class OnlineScreen : IEntity
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public int ScreenId { get; set; }
        public Screen Screen { get; set; }


    }
}
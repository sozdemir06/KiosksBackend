namespace Core.Entities.Concrete
{
    public class HomeAnnounceSubScreen:IEntity
    {
        public int Id { get; set; }
        public SubScreen SubScreen { get; set; }
        public int SubScreenId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
        public HomeAnnounce HomeAnnounce { get; set; }
        public int HomeAnnounceId { get; set; }
        public Screen Screen { get; set; }
        public int ScreenId { get; set; }
    }
}
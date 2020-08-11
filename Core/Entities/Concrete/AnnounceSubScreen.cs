namespace Core.Entities.Concrete
{
    public class AnnounceSubScreen : IEntity
    {
        public int Id { get; set; }
        public SubScreen SubScreen { get; set; }
        public int SubScreenId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
        public Announce Announce { get; set; }
        public int AnnounceId { get; set; }
        public Screen Screen { get; set; }
        public int ScreenId { get; set; }
    }
}
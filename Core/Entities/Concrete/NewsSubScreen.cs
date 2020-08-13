namespace Core.Entities.Concrete
{
    public class NewsSubScreen:IEntity
    {
         public int Id { get; set; }
        public SubScreen SubScreen { get; set; }
        public int SubScreenId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
        public News  News { get; set; }
        public int NewsId { get; set; }
        public Screen Screen { get; set; }
        public int ScreenId { get; set; }
    }
}
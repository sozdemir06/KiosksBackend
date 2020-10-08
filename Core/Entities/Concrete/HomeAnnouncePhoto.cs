namespace Core.Entities.Concrete
{
    public class HomeAnnouncePhoto:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsConfirm { get; set; }
        public bool UnConfirm { get; set; }
        public HomeAnnounce HomeAnnounce { get; set; }
        public int HomeAnnounceId { get; set; }
    }
}
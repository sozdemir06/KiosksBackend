namespace Core.Entities.Concrete
{
    public class NewsPhoto:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsConfirm { get; set; }
        public News News { get; set; }
        public int NewsId { get; set; }
    }
}
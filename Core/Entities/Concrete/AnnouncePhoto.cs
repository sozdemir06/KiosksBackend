namespace Core.Entities.Concrete
{
    public class AnnouncePhoto:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsConfirm { get; set; }
        public Announce Announce { get; set; }
        public int AnnounceId { get; set; }
    }
}
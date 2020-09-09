namespace Core.Entities.Concrete
{
    public class ScreenHeaderPhoto:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsMain { get; set; }
        public string Position { get; set; }
        public Screen Screen { get; set; }
        public int ScreenId { get; set; }
    }
}
namespace Core.Entities.Concrete
{
    public class PublicLogo : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsMain { get; set; }
      
    }
}
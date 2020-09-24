namespace Core.Entities.Concrete
{
    public class UserPhoto:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsMain { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
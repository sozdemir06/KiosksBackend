namespace Core.Entities.Concrete
{
    public class AnnounceContentType:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
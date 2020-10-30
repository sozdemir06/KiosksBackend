namespace Core.Entities.Concrete
{
    public class PublicFooterText:IEntity
    {
        public int Id { get; set; }
        public string FooterText { get; set; }
        public string ContentPhoneNumber { get; set; }
    }
}
using Core.Entities;

namespace Entities.Dtos
{
    public class PublicFooterTextForReturnDto : IDto
    {
        public int Id { get; set; }
        public string FooterText { get; set; }
        public string ContentPhoneNumber { get; set; }
    }
}
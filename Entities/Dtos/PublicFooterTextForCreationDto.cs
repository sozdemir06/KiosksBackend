using Core.Entities;

namespace Entities.Dtos
{
    public class PublicFooterTextForCreationDto : IDto
    {
        public int Id { get; set; }
        public string FooterText { get; set; }
        public string ContentPhoneNumber { get; set; }
    }
}
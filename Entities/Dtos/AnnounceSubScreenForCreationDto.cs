using Core.Entities;

namespace Entities.Dtos
{
    public class AnnounceSubScreenForCreationDto : IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int AnnounceId { get; set; }
        public int ScreenId { get; set; }
    }
}
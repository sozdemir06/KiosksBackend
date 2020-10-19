using Core.Entities;

namespace Entities.Dtos
{
    public class AnnounceSubScreenForReturnDto : IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int ScreenId { get; set; }
        public int AnnounceId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
    }
}
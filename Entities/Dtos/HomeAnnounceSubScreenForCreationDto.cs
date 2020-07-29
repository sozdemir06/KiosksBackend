using Core.Entities;

namespace Entities.Dtos
{
    public class HomeAnnounceSubScreenForCreationDto:IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int HomeAnnounceId { get; set; }
        public int ScreenId { get; set; }
    }
}
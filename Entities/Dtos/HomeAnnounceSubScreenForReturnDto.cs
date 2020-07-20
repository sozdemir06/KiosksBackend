using Core.Entities;

namespace Entities.Dtos
{
    public class HomeAnnounceSubScreenForReturnDto:IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int HomeAnnounceId { get; set; }
    }
}
using Core.Entities;

namespace Entities.Dtos
{
    public class OnlineScreenForReturnDto:IDto
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public int ScreenId { get; set; }
    }
}
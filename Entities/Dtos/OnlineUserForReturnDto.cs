using Core.Entities;

namespace Entities.Dtos
{
    public class OnlineUserForReturnDto : IDto
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public int UserId { get; set; }
    }
}
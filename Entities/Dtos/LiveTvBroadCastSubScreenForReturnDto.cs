using Core.Entities;

namespace Entities.Dtos
{
    public class LiveTvBroadCastSubScreenForReturnDto:IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int LiveTvBroadCastId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
    }
}
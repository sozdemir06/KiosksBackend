using Core.Entities;

namespace Entities.Dtos
{
    public class LiveTvBroadCastSubScreenForCreationDto:IDto
    {
         public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int LiveTvBroadCastId { get; set; }
        public int ScreenId { get; set; }
    }
}
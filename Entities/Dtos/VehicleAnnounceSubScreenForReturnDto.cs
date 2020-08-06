using Core.Entities;

namespace Entities.Dtos
{
    public class VehicleAnnounceSubScreenForReturnDto:IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int VehicleAnnounceId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
    }
}
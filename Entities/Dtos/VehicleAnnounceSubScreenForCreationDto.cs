using Core.Entities;

namespace Entities.Dtos
{
    public class VehicleAnnounceSubScreenForCreationDto : IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int VehicleAnnounceId { get; set; }
        public int ScreenId { get; set; }
    }
}
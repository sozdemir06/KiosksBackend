using Core.Entities;

namespace Entities.Dtos
{
    public class VehicleAnnouncePhotoForReturnDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsConfirm { get; set; }
        public bool UnConfirm { get; set; }
        public int VehicleAnnounceId { get; set; }
    }
}
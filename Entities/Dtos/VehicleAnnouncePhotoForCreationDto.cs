using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class VehicleAnnouncePhotoForCreationDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsConfirm { get; set; }
        public int VehicleAnnounceId { get; set; }
        public IFormFile File { get; set; }
    }
}
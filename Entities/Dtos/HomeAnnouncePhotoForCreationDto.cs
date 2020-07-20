using Core.Entities;

namespace Entities.Dtos
{
    public class HomeAnnouncePhotoForCreationDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsConfirm { get; set; }
        public int HomeAnnounceId { get; set; }
    }
}
using Core.Entities;

namespace Entities.Dtos
{
    public class AnnouncePhotoForReturnDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsConfirm { get; set; }
        public int AnnounceId { get; set; }
        public int Duration { get; set; }
    }
}
using Core.Entities;

namespace Entities.Dtos
{
    public class HomeAnnouncePhotoForReturnDto:IDto
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsConfirm { get; set; }
        public bool UnConfirm { get; set; }
        public int HomeAnnounceId { get; set; }
        public string PhotoUrl { get; set; }
    }
}
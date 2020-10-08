using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class HomeAnnouncePhotoForCreationDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
         public string FileType { get; set; }
        public bool IsConfirm { get; set; }
        public bool UnConfirm { get; set; }
        public int HomeAnnounceId { get; set; }
        public IFormFile File { get; set; }
    }
}
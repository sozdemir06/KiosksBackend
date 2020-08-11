using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class AnnouncePhotoForCretionDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsConfirm { get; set; }
        public int AnnounceId { get; set; }

    }
}
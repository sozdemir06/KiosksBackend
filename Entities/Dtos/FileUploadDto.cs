using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class FileUploadDto:IDto
    {
        public int AnnounceId { get; set; }
        public IFormFile File { get; set; }
        public string FileType { get; set; }
        public int Duration { get; set; }
    }
}
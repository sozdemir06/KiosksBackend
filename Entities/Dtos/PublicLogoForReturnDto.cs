using Core.Entities;

namespace Entities.Dtos
{
    public class PublicLogoForReturnDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsMain { get; set; }
        public string PhotoUrl { get; set; }
    }
}
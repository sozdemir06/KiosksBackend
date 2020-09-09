using Core.Entities;

namespace Entities.Dtos
{
    public class ScreenHeaderPhotoForCreationDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileType { get; set; }
        public bool IsMain { get; set; }
        public string Position { get; set; }
        public int ScreenId { get; set; }
    }
}
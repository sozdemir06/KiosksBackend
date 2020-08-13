using Core.Entities;

namespace Entities.Dtos
{
    public class NewsSubScreenForCreationDto : IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int NewsId { get; set; }
        public int ScreenId { get; set; }
    }
}
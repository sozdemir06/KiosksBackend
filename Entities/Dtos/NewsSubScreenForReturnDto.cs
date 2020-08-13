using Core.Entities;

namespace Entities.Dtos
{
    public class NewsSubScreenForReturnDto : IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int NewsId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
    }
}
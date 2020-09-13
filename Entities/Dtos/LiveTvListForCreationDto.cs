using Core.Entities;

namespace Entities.Dtos
{
    public class LiveTvListForCreationDto : IDto
    {
        public int Id { get; set; }
        public string YoutubeId { get; set; }
        public string TvName { get; set; }
    }
}
using System;

namespace Core.Entities.Concrete
{
    public class LiveTvList:IEntity
    {
        public int Id { get; set; }
        public string YoutubeId { get; set; }
        public string TvName { get; set; }

    }
}
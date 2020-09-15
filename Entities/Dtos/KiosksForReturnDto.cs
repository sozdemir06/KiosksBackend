using System.Collections.Generic;
using Core.Entities;

namespace Entities.Dtos
{
    public class KiosksForReturnDto : IDto
    {
        public ScreenForKiosksToReturnDto Screen { get; set; }
        public List<AnnounceForKiosksToReturnDto> Announces { get; set; }
        public List<HomeAnnounceForKiosksForReturnDto> HomeAnnounces { get; set; }
        public List<VehicleAnnounceForKiosksToReturnDto> VehicleAnnounces { get; set; }
        public List<NewsForKiosksToReturnDto> News { get; set; }
        public List<FoodMenuForKiosksToReturnDto> FoodsMenu { get; set; }
        public List<LiveTvBroadCastForKiosksToReturnDto> LiveTvBroadCasts { get; set; }
    }
}
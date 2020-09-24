using System.Collections.Generic;
using Core.Entities;

namespace Entities.Dtos
{
    public class PublicForReturnDto : IDto
    {
        public List<AnnounceForPublicDto> Announces { get; set; }
        public List<HomeAnnounceForPublicDto> HomeAnnounces { get; set; }
        public List<VehicleAnnounceForPublicDto> VehicleAnnounces { get; set; }
        public List<NewsForPublicDto> News { get; set; }
        public List<FoodMenuForPublicDto> FoodsMenu { get; set; }

    }
}
using System.Collections.Generic;
using Core.Entities;

namespace Entities.Dtos
{
    public class KiosksForReturnDto : IDto
    {
        public ScreenForReturnDto Screen { get; set; }
        public List<SubScreenForReturnDto> SubScreens { get; set; }
        public List<AnnounceForDetailDto> Announces { get; set; }
        public List<HomeAnnounceForDetailDto> HomeAnnounces { get; set; }
        public List<VehicleAnnounceForDetailDto> Vehicleannounces { get; set; }
        public List<NewsForDetailDto> News { get; set; }
        public List<FoodMenuForDetailDto> FoodsMenu { get; set; }
    }
}
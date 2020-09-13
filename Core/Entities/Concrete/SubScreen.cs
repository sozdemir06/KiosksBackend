using System.Collections.Generic;
using Core.Entities;

namespace Core.Entities.Concrete
{
    public class SubScreen : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public bool Status { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int ScreenId { get; set; }
        public Screen Screen { get; set; }

        public ICollection<HomeAnnounceSubScreen> HomeAnnounceSubScreens { get; set; }
        public ICollection<VehicleAnnounceSubScreen> VehicleAnnounceSubScreens { get; set; }
        public ICollection<AnnounceSubScreen> AnnounceSubScreens { get; set; }
        public ICollection<NewsSubScreen> NewsSubScreens { get; set; }
        public ICollection<FoodMenuSubscreen> FoodMenuSubScreens { get; set; }
        public ICollection<LiveTvBroadCastSubScreen> LiveTvBroadCastSubScreens { get; set; }

    }
}
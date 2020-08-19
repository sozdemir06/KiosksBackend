using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Concrete
{
    public class Screen : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public bool IsFull { get; set; }

        public ICollection<SubScreen> SubScreens { get; set; }
        public ICollection<HomeAnnounceSubScreen> HomeAnnounceSubScreens { get; set; }
        public ICollection<VehicleAnnounceSubScreen> VehicleAnnounceSubScreens { get; set; }
        public ICollection<AnnounceSubScreen> AnnounceSubScreens { get; set; }
        public ICollection<NewsSubScreen> NewsSubScreens { get; set; }
        public ICollection<FoodMenuSubscreen> FoodMenuSubScreens { get; set; }

    }
}
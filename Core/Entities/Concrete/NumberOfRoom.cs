using System.Collections.Generic;
using Core.Entities;

namespace Core.Entities.Concrete
{
    public class NumberOfRoom : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<HomeAnnounce> HomeAnnounces { get; set; }
    }
}
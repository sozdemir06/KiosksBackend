using Core.Entities;

namespace Entities.Dtos
{
    public class Weather : IDto
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

    }
}
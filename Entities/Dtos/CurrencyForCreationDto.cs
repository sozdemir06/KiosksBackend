using Core.Entities;

namespace Entities.Dtos
{
    public class CurrencyForCreationDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShorName { get; set; }
        public bool Selected { get; set; }
        public string Symbol { get; set; }
    }
}
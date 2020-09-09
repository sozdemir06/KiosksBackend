using Core.Entities;

namespace Entities.Dtos
{
    public class ScreenFooterForCreationDto : IDto
    {
        public int Id { get; set; }
        public string FooterText { get; set; }
        public bool IsShowWheatherForCast { get; set; }
        public bool IsShowStockExchange { get; set; }
        public int ScreenId { get; set; }
    }
}
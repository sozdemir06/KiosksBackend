namespace Core.Entities.Concrete
{
    public class ScreenFooter:IEntity
    {
        public int Id { get; set; }
        public string FooterText { get; set; }
        public bool IsShowWheatherForCast { get; set; }
        public bool IsShowStockExchange { get; set; }
        public Screen Screen { get; set; }
        public int ScreenId { get; set; }
    }
}
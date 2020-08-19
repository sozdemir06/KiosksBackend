namespace Core.Entities.Concrete
{
    public class FoodMenuSubscreen : IEntity
    {
        public int Id { get; set; }
        public SubScreen SubScreen { get; set; }
        public int SubScreenId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
        public FoodMenu FoodMenu { get; set; }
        public int FoodMenuId { get; set; }
        public Screen Screen { get; set; }
        public int ScreenId { get; set; }
    }
}
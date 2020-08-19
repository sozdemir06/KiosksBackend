using Core.Entities;

namespace Entities.Dtos
{
    public class FoodMenuSubScreenForReturnDto : IDto
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int FoodMenuId { get; set; }
        public string SubScreenName { get; set; }
        public string SubScreenPosition { get; set; }
    }
}
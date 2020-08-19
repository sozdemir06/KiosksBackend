using Core.Entities;

namespace Entities.Dtos
{
    public class FoodMenuSubScreenForCreationDto:IDto
    {
         public int Id { get; set; }
        public int SubScreenId { get; set; }
        public int FoodMenuId { get; set; }
        public int ScreenId { get; set; } 
    }
}
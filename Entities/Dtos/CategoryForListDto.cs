using Core.Entities;

namespace Entities.Dtos
{
    public class CategoryForListDto:IDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
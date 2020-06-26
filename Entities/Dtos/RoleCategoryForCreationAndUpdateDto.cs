using Core.Entities;

namespace Entities.Dtos
{
    public class RoleCategoryForCreationAndUpdateDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
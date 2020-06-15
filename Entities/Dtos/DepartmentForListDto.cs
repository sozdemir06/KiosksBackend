using Core.Entities;

namespace Entities.Dtos
{
    public class DepartmentForListDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
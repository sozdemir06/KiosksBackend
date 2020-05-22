
namespace Core.Entities
{
    public class UserRoleForListDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
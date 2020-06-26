using Core.Entities;

namespace Entities.Dtos
{
    public class UserRolesForListDto:IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
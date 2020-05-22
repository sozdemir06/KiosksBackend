using Core.Entities;

namespace Core.Entities.Concrete
{
    public class UserRole:IEntity
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
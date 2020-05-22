using System.Collections.Generic;
using Core.Entities;

namespace Core.Entities.Concrete
{
    public class RoleCategory:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
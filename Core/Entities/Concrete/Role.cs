using System;
using System.Collections.Generic;


namespace Core.Entities.Concrete
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public RoleCategory RoleCategory { get; set; }
        public int RoleCategoryId { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }


        public Role()
        {
            Created = DateTime.Now;
        }

    }
}
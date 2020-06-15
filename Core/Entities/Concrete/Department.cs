

using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    public class Department:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
         public ICollection<User> Users { get; set; }
    }
}
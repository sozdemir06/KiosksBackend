using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class RoleForListDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public RoleCategoryForListDto RoleCategory { get; set; }
        public int RoleCategoryId { get; set; }
    }
}
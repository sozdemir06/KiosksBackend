using System;
using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Dtos
{
    public class UserForListDto:IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InterPhone { get; set; }
        public string GsmPhone { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }        
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public CampusForReturnDto Campus { get; set; }
        public DepartmentForReturnDto Department { get; set; }
        public DegreeForReturnDto Degree { get; set; }
    }
}
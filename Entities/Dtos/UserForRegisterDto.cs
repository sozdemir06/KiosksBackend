using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class UserForRegisterDto:IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InterPhone { get; set; }
        public string GsmPhone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int CampusId { get; set; }
        public int DepartmentId { get; set; }
        public int DegreeId { get; set; }
        public DateTime Created { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public UserForRegisterDto()
        {
            Created=DateTime.Now;
        }
    }
}
using System.Collections.Generic;
using Core.Entities;

namespace Entities.Dtos
{
    public class UserCamPusAndDepartmentAndDegree:IDto
    {
        public List<CampusForReturnDto> Campuses { get; set; }   
        public List<DepartmentForReturnDto> Departments { get; set; }   
        public List<DegreeForReturnDto> Degrees { get; set; }   
    }
}
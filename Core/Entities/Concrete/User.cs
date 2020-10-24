using System;
using System.Collections.Generic;


namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InterPhone { get; set; }
        public string GsmPhone { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Campus Campus { get; set; }
        public int CampusId { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public Degree Degree { get; set; }
        public int DegreeId { get; set; }

        public ICollection<UserNotifyGroup> UserNotifyGroups { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserPhoto> UserPhotos { get; set; }
        public ICollection<HomeAnnounce> HomeAnnounces { get; set; }
        public ICollection<VehicleAnnounce> VehicleAnnounces { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<FoodMenu> FoodMenus { get; set; }
        public ICollection<Announce> Announces { get; set; }
        public ICollection<LiveTvBroadCast> LiveTvBroadCasts { get; set; }





    }
}
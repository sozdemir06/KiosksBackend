using System;
using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    public class HomeAnnounce : IEntity
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string AnnounceType { get; set; }
        public Guid SlideId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishFinishDate { get; set; }
        public NumberOfRoom NumberOfRoom { get; set; }
        public int NumberOfRoomId { get; set; }
        public HeatingType Heatingtype { get; set; }
        public int HeatingTypeId { get; set; }
        public FlatOfHome FlatOfHome { get; set; }
        public int FlatOfHomeId { get; set; }
        public BuildingAge BuildingAge { get; set; }
        public int BuildingAgeId { get; set; }
        public int Price { get; set; }
        public int SquareMeters { get; set; }
        public int SlideIntervalTime { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool IsNew { get; set; }
        public bool Reject { get; set; }
        public bool IsPublish { get; set; }

        public ICollection<HomeAnnounceSubScreen> HomeAnnounceSubScreens { get; set; }
        public ICollection<HomeAnnouncePhoto> HomeAnnouncePhotos { get; set; }



    }
}
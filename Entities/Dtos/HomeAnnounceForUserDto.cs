using System;
using System.Collections.Generic;
using Core.Entities;

namespace Entities.Dtos
{
    public class HomeAnnounceForUserDto:IDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishFinishDate { get; set; }
        public string NumberOfRoomName { get; set; }
        public int NumberOfRoomId { get; set; }
        public string HeatingtypeName { get; set; }
        public int HeatingTypeId { get; set; }
        public string FlatOfHomeName { get; set; }
        public int FlatOfHomeId { get; set; }
        public string BuildingAgeName { get; set; }
        public int BuildingAgeId { get; set; }
        public int Price { get; set; }
        public int SquareMeters { get; set; }
        public int SlideIntervalTime { get; set; }
        public int UserId { get; set; }
        public bool IsNew { get; set; }
        public bool Reject { get; set; }
        public bool IsPublish { get; set; }
        public UserForListDto User { get; set; }
        public ICollection<HomeAnnouncePhotoForReturnDto> HomeAnnouncePhotos { get; set; }
    }
}
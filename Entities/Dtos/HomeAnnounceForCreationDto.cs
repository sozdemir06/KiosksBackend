using System;
using Core.Entities;


namespace Entities.Dtos
{
    public class HomeAnnounceForCreationDto : IDto
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
        public int NumberOfRoomId { get; set; }
        public int HeatingTypeId { get; set; }
        public int FlatOfHomeId { get; set; }
        public int BuildingAgeId { get; set; }
        public int Price { get; set; }
        public int SquareMeters { get; set; }
        public int UserId { get; set; }
        public bool IsNew { get; set; }
        public bool Reject { get; set; }
        public bool IsPublish { get; set; }

        public HomeAnnounceForCreationDto()
        {

            AnnounceType="Home";

        }


    }
}
using System;
using Core.Entities;
using Core.Entities.Concrete;

namespace Core.Entities.Concrete
{
    public class HomeAnnounce:IEntity
    {
       public int Id { get; set; }
       public string Header { get; set; }
       public string Description { get; set; }
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
       public Screen Screen { get; set; }
       public int ScreenId { get; set; }
       public int BuildingAgeId { get; set; }
       public decimal Price { get; set; }
       public int SquareMeters { get; set; }
       public User User { get; set; }
       public int UserId { get; set; }
       public bool IsNew { get; set; }
       public bool Reject { get; set; }
       public bool IsPublish { get; set; }



    }
}
using System;
using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    public class VehicleAnnounce : IEntity
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
        public VehicleCategory VehicleCategory { get; set; }
        public int VehicleCategoryId { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public int VehicleBrandId { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public int VehicleModelId { get; set; }
        public VehicleFuelType VehicleFuelType { get; set; }
        public int VehicleFuelTypeId { get; set; }
        public VehicleGearType VehicleGearType { get; set; }
        public int VehicleGearTypeId { get; set; }
        public VehicleEngineSize VehicleEngineSize { get; set; }
        public int VehicleEngineSizeId { get; set; }
        public int Price { get; set; }
        public int SquareMeters { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool IsNew { get; set; }
        public bool Reject { get; set; }
        public bool IsPublish { get; set; }

        public ICollection<VehicleAnnouncePhoto> VehicleAnnouncePhotos { get; set; }
        public ICollection<VehicleAnnounceSubScreen> VehicleAnnounceSubScreens { get; set; }

    }
}
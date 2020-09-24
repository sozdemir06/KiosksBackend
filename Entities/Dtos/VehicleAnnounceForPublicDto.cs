using System;
using System.Collections.Generic;
using Core.Entities;

namespace Entities.Dtos
{
    public class VehicleAnnounceForPublicDto:IDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string AnnounceType { get; set; }
        public string PhotoUrl { get; set; }
        public Guid SlideId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishFinishDate { get; set; }
        public string VehicleCategoryName { get; set; }
        public int VehicleCategoryId { get; set; }
        public string VehicleBrandName { get; set; }
        public int VehicleBrandId { get; set; }
        public string VehicleModelName { get; set; }
        public int VehicleModelId { get; set; }
        public string VehicleFuelTypeName { get; set; }
        public int VehicleFuelTypeId { get; set; }
        public string VehicleGearTypeName { get; set; }
        public int VehicleGearTypeId { get; set; }
        public string VehicleEngineSizeName { get; set; }
        public int VehicleEngineSizeId { get; set; }
        public int SlideIntervalTime { get; set; }
        public int Price { get; set; }
        public bool IsNew { get; set; }
        public bool Reject { get; set; }
        public bool IsPublish { get; set; }
        public UserForListDto User { get; set; }
        public ICollection<VehicleAnnouncePhotoForReturnDto> VehicleAnnouncePhotos { get; set; }
    }
}
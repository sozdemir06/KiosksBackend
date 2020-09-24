using System;
using System.Collections.Generic;
using Core.Entities;

namespace Entities.Dtos
{
    public class FoodMenuForPublicDto:IDto
    {
         public int Id { get; set; }
        public string AnnounceType { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public Guid SlideId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishFinishDate { get; set; }
        public int UserId { get; set; }
        public int SlideIntervalTime { get; set; }
        public bool IsNew { get; set; }
        public bool Reject { get; set; }
        public bool IsPublish { get; set; }
        public UserForListDto User { get; set; }
        public ICollection<FoodMenuPhotoForReturnDto> FoodMenuPhotos { get; set; }

    }
}
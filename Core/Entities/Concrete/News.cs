using System;
using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    public class News:IEntity
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string AnnounceType { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public Guid SlideId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishFinishDate { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int SlideIntervalTime { get; set; }
        public bool IsNew { get; set; }
        public bool Reject { get; set; }
        public bool IsPublish { get; set; }
        public ICollection<NewsPhoto> NewsPhotos { get; set; }
        public ICollection<NewsSubScreen> NewsSubScreens { get; set; }
    }
}
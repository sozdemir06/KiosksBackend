using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class LiveTvBroadCastConfiguration : IEntityTypeConfiguration<LiveTvBroadCast>
    {
        public void Configure(EntityTypeBuilder<LiveTvBroadCast> builder)
        {
           builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Header).IsRequired().HasMaxLength(140);
            builder.Property(x=>x.YoutubeId).IsRequired();
            builder.Property(x=>x.AnnounceType);
            builder.Property(x=>x.PublishFinishDate);
            builder.Property(x=>x.PublishStartDate);
            builder.Property(x=>x.Reject);
            builder.Property(x=>x.IsNew);
            builder.Property(x=>x.IsPublish);
            builder.Property(x=>x.Updated);
            builder.Property(x=>x.UserId);
            builder.Property(x=>x.Created); 
        }
    }
}
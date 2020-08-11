using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class AnnounceConfiguration : IEntityTypeConfiguration<Announce>
    {
        public void Configure(EntityTypeBuilder<Announce> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Header).IsRequired().HasMaxLength(140);
            builder.Property(x=>x.Content);
            builder.Property(x=>x.ContentType).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.PublishFinishDate);
            builder.Property(x=>x.PublishStartDate);
            builder.Property(x=>x.Reject);
            builder.Property(x=>x.IsNew);
            builder.Property(x=>x.IsPublish);
            builder.Property(x=>x.Updated);
            builder.Property(x=>x.UserId);
            builder.Property(x=>x.Created);

            //Fluent Api
            
        }
    }
}
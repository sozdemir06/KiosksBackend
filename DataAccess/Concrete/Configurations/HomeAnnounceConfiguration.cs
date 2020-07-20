using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class HomeAnnounceConfiguration : IEntityTypeConfiguration<HomeAnnounce>
    {
        public void Configure(EntityTypeBuilder<HomeAnnounce> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Header).IsRequired().HasMaxLength(140);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x=>x.HeatingTypeId);
            builder.Property(x=>x.NumberOfRoomId);
            builder.Property(x=>x.PublishFinishDate);
            builder.Property(x=>x.PublishStartDate);
            builder.Property(x=>x.Reject);
            builder.Property(x=>x.Updated);
            builder.Property(x=>x.UserId);
            builder.Property(x=>x.BuildingAgeId);
            builder.Property(x=>x.Created);

            //Fluent Api
            builder.HasOne(x=>x.NumberOfRoom).WithMany(x=>x.HomeAnnounces).HasForeignKey(x=>x.NumberOfRoomId);
            builder.HasOne(x=>x.Heatingtype).WithMany(x=>x.HomeAnnounces).HasForeignKey(x=>x.HeatingTypeId);
            builder.HasOne(x=>x.FlatOfHome).WithMany(x=>x.HomeAnnounces).HasForeignKey(x=>x.FlatOfHomeId);
            builder.HasOne(x=>x.BuildingAge).WithMany(x=>x.HomeAnnounces).HasForeignKey(x=>x.BuildingAgeId);

            


        }
    }
}
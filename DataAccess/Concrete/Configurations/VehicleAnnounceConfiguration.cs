using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class VehicleAnnounceConfiguration : IEntityTypeConfiguration<VehicleAnnounce>
    {
        public void Configure(EntityTypeBuilder<VehicleAnnounce> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Header).IsRequired().HasMaxLength(140);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x=>x.VehicleCategoryId);
            builder.Property(x=>x.VehicleBrandId);
            builder.Property(x=>x.VehicleModelId);
            builder.Property(x=>x.VehicleFuelTypeId);
            builder.Property(x=>x.VehicleGearTypeId);
            builder.Property(x=>x.VehicleEngineSizeId);

            //Fluent API
            builder.HasOne(x=>x.VehicleCategory).WithMany(x=>x.VehicleAnnounces).HasForeignKey(x=>x.VehicleCategoryId);
            builder.HasOne(x=>x.VehicleBrand).WithMany(x=>x.VehicleAnnounces).HasForeignKey(x=>x.VehicleBrandId);
            builder.HasOne(x=>x.VehicleModel).WithMany(x=>x.VehicleAnnounces).HasForeignKey(x=>x.VehicleModelId);
            builder.HasOne(x=>x.VehicleFuelType).WithMany(x=>x.VehicleAnnounces).HasForeignKey(x=>x.VehicleFuelTypeId);
            builder.HasOne(x=>x.VehicleGearType).WithMany(x=>x.VehicleAnnounces).HasForeignKey(x=>x.VehicleGearTypeId);
            builder.HasOne(x=>x.VehicleEngineSize).WithMany(x=>x.VehicleAnnounces).HasForeignKey(x=>x.VehicleEngineSizeId);
        }
    }
}
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class VehicleAnnounceSubScreenConfiguration : IEntityTypeConfiguration<VehicleAnnounceSubScreen>
    {
        public void Configure(EntityTypeBuilder<VehicleAnnounceSubScreen> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(X=>X.SubScreenId);
            builder.Property(X=>X.VehicleAnnounceId);

            //Fluent Api
            builder.HasOne(x=>x.VehicleAnnounce).WithMany(x=>x.VehicleAnnounceSubScreens).HasForeignKey(x=>x.VehicleAnnounceId);
            builder.HasOne(x=>x.SubScreen).WithMany(x=>x.VehicleAnnounceSubScreens).HasForeignKey(x=>x.SubScreenId);
            builder.HasOne(x=>x.Screen).WithMany(x=>x.VehicleAnnounceSubScreens).HasForeignKey(x=>x.ScreenId);
        }
    }
}
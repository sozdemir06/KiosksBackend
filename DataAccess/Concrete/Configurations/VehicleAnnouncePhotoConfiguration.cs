using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class VehicleAnnouncePhotoConfiguration : IEntityTypeConfiguration<VehicleAnnouncePhoto>
    {
        public void Configure(EntityTypeBuilder<VehicleAnnouncePhoto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.IsConfirm);
            builder.Property(x => x.FullPath);

            //Fluent Api
            builder.HasOne(x => x.VehicleAnnounce).WithMany(x => x.VehicleAnnouncePhotos);
        }
    }
}
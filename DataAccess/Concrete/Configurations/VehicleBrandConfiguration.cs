using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class VehicleBrandConfiguration : IEntityTypeConfiguration<VehicleBrand>
    {
        public void Configure(EntityTypeBuilder<VehicleBrand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasIdentityOptions(startValue: 115);
            builder.Property(x => x.BrandName).IsRequired().HasMaxLength(60);
            builder.Property(x => x.VehicleCategoryId).IsRequired();

            builder.HasOne(x => x.VehicleCategories)
                    .WithMany(x => x.VehicleBrands)
                    .HasForeignKey(fk => fk.VehicleCategoryId);

            builder.HasMany(x => x.VehicleModels)
                   .WithOne(x => x.VehicleBrands)
                   .HasForeignKey(x => x.VehicleBrandId);

        }
    }
}
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
    {
        public void Configure(EntityTypeBuilder<VehicleModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasIdentityOptions(startValue: 825);
            builder.Property(x => x.VehicleModelName).IsRequired().HasMaxLength(60);
            builder.Property(x => x.VehicleCategoryId).IsRequired();
            builder.Property(x => x.VehicleBrandId).IsRequired();

             builder.HasOne(x=>x.VehicleCategories).WithMany(x=>x.VehicleModels)
                    .HasForeignKey(fk=>fk.VehicleCategoryId);
           
             
            
        }
    }
}
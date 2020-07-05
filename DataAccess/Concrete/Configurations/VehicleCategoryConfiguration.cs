using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class VehicleCategoryConfiguration : IEntityTypeConfiguration<VehicleCategory>
    {
        public void Configure(EntityTypeBuilder<VehicleCategory> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).HasIdentityOptions(startValue:5);
            builder.Property(x=>x.CategoryName).HasMaxLength(60).IsRequired();
        }
    }
}
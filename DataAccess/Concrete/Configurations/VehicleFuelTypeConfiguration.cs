using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class VehicleFuelTypeConfiguration : IEntityTypeConfiguration<VehicleFuelType>
    {
        public void Configure(EntityTypeBuilder<VehicleFuelType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();
        }
    }
}
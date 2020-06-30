using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class BuildingAgeConfiguration : IEntityTypeConfiguration<BuildingAge>
    {
        public void Configure(EntityTypeBuilder<BuildingAge> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Name).HasMaxLength(60).IsRequired();
        }
    }
}
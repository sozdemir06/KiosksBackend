using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class HeatingTypeConfiguration : IEntityTypeConfiguration<HeatingType>
    {
        public void Configure(EntityTypeBuilder<HeatingType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();
        }
    }
}
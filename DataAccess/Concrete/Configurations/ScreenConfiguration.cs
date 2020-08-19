using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class ScreenConfiguration : IEntityTypeConfiguration<Screen>
    {
        public void Configure(EntityTypeBuilder<Screen> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).HasIdentityOptions(startValue:10);
            builder.Property(x=>x.Name).IsRequired();
            builder.Property(x=>x.Position);
        }
    }
}
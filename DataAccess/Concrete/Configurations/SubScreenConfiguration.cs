using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class SubScreenConfiguration : IEntityTypeConfiguration<SubScreen>
    {
        public void Configure(EntityTypeBuilder<SubScreen> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Position).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Width);
            builder.Property(x => x.Height);
            builder.Property(x => x.Status);
            builder.Property(x => x.ScreenId);

            builder.HasOne(x=>x.Screen)
            .WithMany(x=>x.SubScreens)
            .HasForeignKey(fk=>fk.ScreenId);

        }
    }
}
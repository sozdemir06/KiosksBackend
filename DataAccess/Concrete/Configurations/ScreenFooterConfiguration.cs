using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class ScreenFooterConfiguration : IEntityTypeConfiguration<ScreenFooter>
    {
        public void Configure(EntityTypeBuilder<ScreenFooter> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.FooterText).HasMaxLength(70);
            builder.Property(x=>x.IsShowStockExchange);
            builder.Property(x=>x.IsShowWheatherForCast);

            //Fluent Api
            builder.HasOne(x=>x.Screen)
                   .WithOne(x=>x.ScreenFooters)
                   .HasForeignKey<ScreenFooter>(fk=>fk.ScreenId);
        }
    }
}
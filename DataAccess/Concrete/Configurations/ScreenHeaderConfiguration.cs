using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class ScreenHeaderConfiguration : IEntityTypeConfiguration<ScreenHeader>
    {
        public void Configure(EntityTypeBuilder<ScreenHeader> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.HeaderText).HasMaxLength(70);

            //Fluent Api
            builder.HasOne(x=>x.Screen)
                   .WithOne(x=>x.ScreenHeaders)
                   .HasForeignKey<ScreenHeader>(fk=>fk.ScreenId);
                
        }
    }
}
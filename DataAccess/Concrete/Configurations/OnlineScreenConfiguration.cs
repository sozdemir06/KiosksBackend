using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class OnlineScreenConfiguration : IEntityTypeConfiguration<OnlineScreen>
    {
        public void Configure(EntityTypeBuilder<OnlineScreen> builder)
        {
            builder.Property(x=>x.ConnectionId);
            builder.Property(x=>x.ScreenId);

            //Fluent Api
            builder.HasOne(x=>x.Screen).WithMany(x=>x.OnlineScreens).HasForeignKey(x=>x.ScreenId);
        }
    }
}
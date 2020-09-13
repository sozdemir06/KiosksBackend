using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class LiveTvBroadCastSubScreenConfiguration : IEntityTypeConfiguration<LiveTvBroadCastSubScreen>
    {
        public void Configure(EntityTypeBuilder<LiveTvBroadCastSubScreen> builder)
        {
             builder.HasKey(x=>x.Id);
            builder.Property(X=>X.SubScreenId);
            builder.Property(X=>X.LiveTvBroadCastId);

            //Fluent Api
            builder.HasOne(x=>x.LiveTvBroadCast).WithMany(x=>x.LiveTvBroadCastSubScreens).HasForeignKey(x=>x.LiveTvBroadCastId);
            builder.HasOne(x=>x.SubScreen).WithMany(x=>x.LiveTvBroadCastSubScreens).HasForeignKey(x=>x.SubScreenId);
            builder.HasOne(x=>x.Screen).WithMany(x=>x.LiveTvBroadCastSubScreens).HasForeignKey(x=>x.ScreenId);
        }
    }
}
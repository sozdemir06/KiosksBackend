using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class HomeAnnounceSubScreenConfigurarion : IEntityTypeConfiguration<HomeAnnounceSubScreen>
    {
        public void Configure(EntityTypeBuilder<HomeAnnounceSubScreen> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(X=>X.SubScreenId);
            builder.Property(X=>X.HomeAnnounceId);

            //Fluent Api
            builder.HasOne(x=>x.HomeAnnounce).WithMany(x=>x.HomeAnnounceSubScreens).HasForeignKey(x=>x.HomeAnnounceId);
            builder.HasOne(x=>x.SubScreen).WithMany(x=>x.HomeAnnounceSubScreens).HasForeignKey(x=>x.SubScreenId);
            builder.HasOne(x=>x.Screen).WithMany(x=>x.HomeAnnounceSubScreens).HasForeignKey(x=>x.ScreenId);
        }
    }
}
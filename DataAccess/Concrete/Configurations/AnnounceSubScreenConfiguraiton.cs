using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class AnnounceSubScreenConfiguraiton : IEntityTypeConfiguration<AnnounceSubScreen>
    {
        public void Configure(EntityTypeBuilder<AnnounceSubScreen> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(X=>X.SubScreenId);
            builder.Property(X=>X.AnnounceId);

            //Fluent Api
            builder.HasOne(x=>x.Announce).WithMany(x=>x.AnnounceSubScreens).HasForeignKey(x=>x.AnnounceId);
            builder.HasOne(x=>x.SubScreen).WithMany(x=>x.AnnounceSubScreens).HasForeignKey(x=>x.SubScreenId);
            builder.HasOne(x=>x.Screen).WithMany(x=>x.AnnounceSubScreens).HasForeignKey(x=>x.ScreenId);
        }
    }
}
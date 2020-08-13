using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class NewsSubScreenConfiguration : IEntityTypeConfiguration<NewsSubScreen>
    {
        public void Configure(EntityTypeBuilder<NewsSubScreen> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(X=>X.SubScreenId);
            builder.Property(X=>X.NewsId);

            //Fluent Api
            builder.HasOne(x=>x.News).WithMany(x=>x.NewsSubScreens).HasForeignKey(x=>x.NewsId);
            builder.HasOne(x=>x.SubScreen).WithMany(x=>x.NewsSubScreens).HasForeignKey(x=>x.SubScreenId);
            builder.HasOne(x=>x.Screen).WithMany(x=>x.NewsSubScreens).HasForeignKey(x=>x.ScreenId);
        }
    }
}
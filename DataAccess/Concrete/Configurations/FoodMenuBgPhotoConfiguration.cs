using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class FoodMenuBgPhotoConfiguration : IEntityTypeConfiguration<FoodMenuBgPhoto>
    {
        public void Configure(EntityTypeBuilder<FoodMenuBgPhoto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.IsSetBackground);
            builder.Property(x => x.FullPath);
        }
    }
}
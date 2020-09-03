using Core.Entities;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class FoodMenuPhotoConfiguration : IEntityTypeConfiguration<FoodMenuPhoto>
    {
        public void Configure(EntityTypeBuilder<FoodMenuPhoto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.IsConfirm);
            builder.Property(x => x.FullPath);

            //Fluent Api
            builder.HasOne(x => x.FoodMenu).WithMany(x => x.FoodMenuPhotos);
        }
    }
}
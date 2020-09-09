using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class ScreenHeaderPhotoConfiguration : IEntityTypeConfiguration<ScreenHeaderPhoto>
    {
        public void Configure(EntityTypeBuilder<ScreenHeaderPhoto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.FullPath);
            builder.Property(x => x.IsMain);
            builder.Property(x => x.ScreenId);
            builder.Property(x => x.Position);


            //Fluent Api
            builder.HasOne(x => x.Screen)
                   .WithMany(x => x.ScreenHeaderPhotos)
                   .HasForeignKey(fk => fk.ScreenId);
        }
    }
}
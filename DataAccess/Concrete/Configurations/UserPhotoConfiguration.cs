using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class UserPhotoConfiguration : IEntityTypeConfiguration<UserPhoto>
    {
        public void Configure(EntityTypeBuilder<UserPhoto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.IsConfirm);
            builder.Property(x => x.IsMain);
            builder.Property(x => x.FullPath);

            //Fluent Api
            builder.HasOne(x => x.User).WithMany(x => x.UserPhotos);
        }
    }
}
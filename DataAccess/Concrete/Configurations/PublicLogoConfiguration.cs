using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class PublicLogoConfiguration : IEntityTypeConfiguration<PublicLogo>
    {
        public void Configure(EntityTypeBuilder<PublicLogo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.FullPath);
            builder.Property(x => x.IsMain);
        }
    }
}
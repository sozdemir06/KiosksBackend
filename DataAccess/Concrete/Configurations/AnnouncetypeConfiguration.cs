using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class AnnouncetypeConfiguration : IEntityTypeConfiguration<AnnounceContentType>
    {
        public void Configure(EntityTypeBuilder<AnnounceContentType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasIdentityOptions(startValue: 40);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
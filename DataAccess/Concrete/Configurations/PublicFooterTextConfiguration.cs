using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class PublicFooterTextConfiguration : IEntityTypeConfiguration<PublicFooterText>
    {
        public void Configure(EntityTypeBuilder<PublicFooterText> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.ContentPhoneNumber);
            builder.Property(x=>x.FooterText);
        }
    }
}
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class FlatOfHomeConfiguration : IEntityTypeConfiguration<FlatOfHome>
    {
        public void Configure(EntityTypeBuilder<FlatOfHome> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Name).HasMaxLength(60).IsRequired();
        }
    }
}
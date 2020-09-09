using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Name);
            builder.Property(x=>x.Selected);
            builder.Property(x=>x.ShorName);
            builder.Property(x=>x.Symbol);
        }
    }
}
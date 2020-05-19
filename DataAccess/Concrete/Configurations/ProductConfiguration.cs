using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p=>p.ProductId);
            builder.Property(p=>p.ProductName).HasMaxLength(60).IsRequired();
            builder.Property(p=>p.Price).HasColumnType("decimal(18,2)");
            
            //Fluent Api
            builder.HasOne(c=>c.Category).WithMany(p=>p.Products)
                   .HasForeignKey(fk=>fk.CategoryId); 
        }
    }
}
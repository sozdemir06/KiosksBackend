using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class RoleCategoryConfiguration : IEntityTypeConfiguration<RoleCategory>
    {
        public void Configure(EntityTypeBuilder<RoleCategory> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Name).HasMaxLength(60);
        }
    }
}
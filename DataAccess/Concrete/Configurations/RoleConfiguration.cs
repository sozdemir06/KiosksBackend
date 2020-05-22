using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Name).HasMaxLength(30);
            builder.Property(x=>x.Description).HasMaxLength(140);

            builder.HasOne(x=>x.RoleCategory)
                    .WithMany(x=>x.Roles)
                    .HasForeignKey(fk=>fk.RoleCategoryId);
        }
    }
}
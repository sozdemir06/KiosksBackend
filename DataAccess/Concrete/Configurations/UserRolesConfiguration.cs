using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur=>ur.Id);

            builder.Property(ur=>ur.RoleId);
            builder.Property(ur=>ur.UserId);
            
            builder.HasOne(u=>u.User)
                   .WithMany(ur=>ur.UserRoles)
                   .HasForeignKey(fk=>fk.UserId);
            builder.HasOne(u=>u.Role)
                   .WithMany(ur=>ur.UserRoles)
                   .HasForeignKey(fk=>fk.RoleId);
             
        }
    }
}
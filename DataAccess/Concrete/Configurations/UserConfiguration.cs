using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(25);
            builder.Property(u => u.LastName).HasMaxLength(30);
            builder.Property(u => u.Email);
            builder.Property(u => u.GsmPhone).HasMaxLength(11);
            builder.Property(u => u.InterPhone).HasMaxLength(11);

            //Fluent Api
            builder.HasOne(x => x.Campus).WithMany(x => x.Users)
                   .HasForeignKey(fk => fk.CampusId);
            builder.HasOne(x => x.Department).WithMany(x => x.Users)
                   .HasForeignKey(fk => fk.DepartmentId);
            builder.HasOne(x => x.Degree).WithMany(x => x.Users)
                   .HasForeignKey(fk => fk.DegreeId);

            builder.HasMany(x=>x.HomeAnnounces).WithOne(x=>x.User).HasForeignKey(x=>x.UserId);
    


        }
    }
}
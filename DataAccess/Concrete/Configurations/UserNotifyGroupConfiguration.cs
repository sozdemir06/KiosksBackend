using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class UserNotifyGroupConfiguration : IEntityTypeConfiguration<UserNotifyGroup>
    {
        public void Configure(EntityTypeBuilder<UserNotifyGroup> builder)
        {
            builder.Property(x=>x.UserId);
            builder.Property(x=>x.NotifyGroupId);

            //Fluent API
            builder.HasOne(x=>x.User)
                   .WithMany(x=>x.UserNotifyGroups)
                   .HasForeignKey(x=>x.UserId);
            builder.HasOne(x=>x.NotifyGroup)
                   .WithMany(x=>x.UserNotifyGroups)
                   .HasForeignKey(x=>x.NotifyGroupId);
        }
    }
}
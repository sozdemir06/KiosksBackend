using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class OnlineUserConfiguration : IEntityTypeConfiguration<OnlineUser>
    {
        public void Configure(EntityTypeBuilder<OnlineUser> builder)
        {
            builder.Property(x=>x.ConnectionId);
            builder.Property(x=>x.UserId);

            //Fluent Api
            builder.HasOne(x=>x.User).WithMany(x=>x.OnlineUsers).HasForeignKey(x=>x.UserId);
        }
    }
}
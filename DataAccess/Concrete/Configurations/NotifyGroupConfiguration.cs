using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class NotifyGroupConfiguration : IEntityTypeConfiguration<NotifyGroup>
    {
        public void Configure(EntityTypeBuilder<NotifyGroup> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Description);
            builder.Property(x=>x.GroupName);
          

            //FluentAPI
       

        }
    }
}
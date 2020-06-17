using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class DegreeConfigurations : IEntityTypeConfiguration<Degree>
    {
        public void Configure(EntityTypeBuilder<Degree> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Name).HasMaxLength(140);

        }
    }
}
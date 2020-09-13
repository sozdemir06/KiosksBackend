using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class LiveTvListConfiguration : IEntityTypeConfiguration<LiveTvList>
    {
        public void Configure(EntityTypeBuilder<LiveTvList> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.TvName);
            builder.Property(x=>x.YoutubeId);
        }
    }
}
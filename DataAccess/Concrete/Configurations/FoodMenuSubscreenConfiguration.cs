using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class FoodMenuSubscreenConfiguration : IEntityTypeConfiguration<FoodMenuSubscreen>
    {
        public void Configure(EntityTypeBuilder<FoodMenuSubscreen> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(X => X.SubScreenId);
            builder.Property(X => X.FoodMenuId);

            //Fluent Api
            builder.HasOne(x => x.FoodMenu).WithMany(x => x.FoodMenuSubScreens).HasForeignKey(x => x.FoodMenuId);
            builder.HasOne(x => x.SubScreen).WithMany(x => x.FoodMenuSubScreens).HasForeignKey(x => x.SubScreenId);
            builder.HasOne(x => x.Screen).WithMany(x => x.FoodMenuSubScreens).HasForeignKey(x => x.ScreenId);
        }
    }
}
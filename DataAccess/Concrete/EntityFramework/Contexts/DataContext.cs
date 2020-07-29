using System.Reflection;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class DataContext : DbContext
    {

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=Hmkiosks;Username=postgres;Password=466357",b=>b.MigrationsAssembly("DataAccess"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleCategory> RoleCategories { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<NumberOfRoom> NumberOfRooms { get; set; }
        public DbSet<BuildingAge> BuildingsAge { get; set; }
        public DbSet<FlatOfHome> FlatsOfHome { get; set; }
        public DbSet<HeatingType> HeatingTypes { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleFuelType> VehicleFuelTypes { get; set; }
        public DbSet<VehicleGearType> VehicleGearTypes { get; set; }
        public DbSet<VehicleEngineSize> VehicleEngineSizes { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<SubScreen> SubScreens { get; set; }
        public DbSet<HomeAnnounce> HomeAnnounces { get; set; }
        public DbSet<HomeAnnounceSubScreen> HomeAnnounceSubScreens { get; set; }
        public DbSet<HomeAnnouncePhoto> HomeAnnouncePhotos { get; set; }
        public DbSet<VehicleAnnounce> VehicleAnnounces { get; set; }
        public DbSet<VehicleAnnouncePhoto> VehicleAnnouncePhotos { get; set; }
        public DbSet<VehicleAnnounceSubScreen> VehicleAnnounceSubScreens { get; set; }
    }
}
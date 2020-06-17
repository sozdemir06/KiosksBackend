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
    }
}
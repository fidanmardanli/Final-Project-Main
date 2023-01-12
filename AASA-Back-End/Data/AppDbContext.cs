using AASA_Back_End.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AASA_Back_End.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Employee> Employeees { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Portfolio>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Setting>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Type>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.IsDeleted);

        }
    }
}

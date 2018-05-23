using EFCore.Data.Maps;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            optionsBuilder.UseSqlServer(@"Server=tcp:192.168.25.23,1401;Database=EFCore;User ID=sa;Password=ervamate@00;");
            optionsBuilder.UseMySQL("server=192.168.25.23;database=EFCore;user=root;password=ervamate;SslMode=none");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
        }
    }
}
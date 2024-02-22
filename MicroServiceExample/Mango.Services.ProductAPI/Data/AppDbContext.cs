using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Mango.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Bajaj Fans",
                Price = 2000,
                Description = "This is an Fan manufactured by Bajaj",
                ImageUrl = "https://placehold.co/603x403",
                CatgoryName = "Bajaj"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Bajaj Cooler",
                Price = 8000,
                Description = "This is an Cooler manufactured by Bajaj",
                ImageUrl = "https://placehold.co/602x402",
                CatgoryName = "Bajaj"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Whirlpool washing Machine",
                Price = 17000,
                Description = "This is an Washing Machine manufactured by Whirlpool",
                ImageUrl = "https://placehold.co/601x401",
                CatgoryName = "Whirlpool"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Hitachi AC",
                Price = 25000,
                Description = "This is an AC manufactured by Hitachi",
                ImageUrl = "https://placehold.co/600x400",
                CatgoryName = "Hitachi"
            });
        }
    }
}

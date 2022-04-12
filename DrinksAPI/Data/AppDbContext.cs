using DrinksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DrinksAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Drink>? Drinks { get; set; }
        public DbSet<Restaurant>? Restaurants { get; set; }
        public DbSet<RestaurantDrink>? rdRelation { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RestaurantDrink>()
                .HasKey(rd => new { rd.RestaurantId, rd.DrinkId });
        }
    }
}

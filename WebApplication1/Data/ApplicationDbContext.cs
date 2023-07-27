using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext , IDbContext<ApplicationDbContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Videogame> Videogames { get ; set ; }
        public DbSet<Genre> Genres { get; set ; }
        public DbSet<ShoppingCart> ShoppingCarts { get ; set ; }
        public DbSet<CartDetail> CartDetails { get ; set; }
        public DbSet<Order> Orders { get ; set ; }
        public DbSet<OrderDetail> OrderDetails { get    ; set ; }
        public DbSet<OrderStatus> OrderStatus { get     ; set   ; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
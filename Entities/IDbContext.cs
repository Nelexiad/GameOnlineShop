using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Entities
{
    public interface IDbContext<T>
    {
        public DbSet<Videogame> Videogames { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<CartDetail> CartDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderStatus> OrderStatus { get; set; }

        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}

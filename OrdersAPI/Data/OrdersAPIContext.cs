using Microsoft.EntityFrameworkCore;

namespace OrdersAPI.Data
{
    public class OrdersAPIContext : DbContext
    {
        public OrdersAPIContext(DbContextOptions<OrdersAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Orders> Orders { get; set; } = default!;

        public DbSet<Product> Products { get; set; } = default!;
    }
}

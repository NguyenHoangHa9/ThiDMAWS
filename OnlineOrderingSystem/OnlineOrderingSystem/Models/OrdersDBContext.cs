using Microsoft.EntityFrameworkCore;

namespace OnlineOrderingSystem.Models
{
    public class OrdersDBContext : DbContext
    {
        

       public OrdersDBContext(DbContextOptions<OrdersDBContext> options) : base(options)
        { 


        }
        public DbSet<Orders> OrderTbl { get; set; } = null!;
    }
}


using Microsoft.EntityFrameworkCore;
using OnlineShop.Products.Models;

namespace OnlineShop.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
    }
}

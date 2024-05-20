using Microsoft.EntityFrameworkCore;
using OnlineShop.Customers.Models;
using OnlineShop.Options.Models;
using OnlineShop.OrderDetails.Models;
using OnlineShop.ProductOptions.Model;
using OnlineShop.Products.Models;
namespace OnlineShop.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductOption> ProductOptions { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderDetail>()
               .HasOne(a => a.Product)
               .WithMany(s => s.OrderDetails)
               .HasForeignKey(a => a.ProductId)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<OrderDetail>()
               .HasOne(a => a.Order)
               .WithMany(s => s.OrderDetails)
               .HasForeignKey(a => a.OrderId)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Order>()
               .HasOne(a => a.Customer)
               .WithMany(s => s.Orders)
               .HasForeignKey(a => a.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ProductOption>()
               .HasOne(a => a.Product)
               .WithMany(s => s.ProductOptions)
               .HasForeignKey(a => a.IdProduct)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ProductOption>()
               .HasOne(a => a.Option)
               .WithOne(s => s.ProductOption)
               .HasForeignKey<ProductOption>(a => a.IdOption)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

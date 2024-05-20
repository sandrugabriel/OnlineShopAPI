using OnlineShop.OrderDetails.Models;
using OnlineShop.ProductOptions.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Products.Models
{
    public class Product
    {
        /* .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Price").AsInt32().NotNullable()
                .WithColumn("Category").AsString().NotNullable()
                .WithColumn("Create_date").AsDateTime().NotNullable()
                .WithColumn("Stock").AsInt32().NotNullable();*/

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime Create_date { get; set; }

        [Required]
        public int Stock { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        public virtual List<ProductOption> ProductOptions { get; set; }

        
    }
}

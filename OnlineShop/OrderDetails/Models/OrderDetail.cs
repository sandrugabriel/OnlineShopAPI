using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OnlineShop.Products.Models;

namespace OnlineShop.OrderDetails.Models
{
    public class OrderDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }


    }
}

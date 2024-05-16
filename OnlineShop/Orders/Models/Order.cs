using OnlineShop.Customers.Models;
using OnlineShop.Products.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using System.Text.Json.Serialization;

namespace OnlineShop.OrderDetails.Models
{
    public class Order
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        [Required]
        public double Ammount { get; set; }

        [Required]
        public string OrderAddress { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }


    }
}

using OnlineShop.Options.Models;
using OnlineShop.Products.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineShop.ProductOptions.Model
{
    public class ProductOption
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("IdOption")]
        public int IdOption { get; set; }

        [JsonIgnore]
        public virtual Option Option { get; set; }

        [ForeignKey("IdProduct")]
        public int IdProduct { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }



    }
}

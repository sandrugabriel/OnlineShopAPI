using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OnlineShop.ProductOptions.Model;

namespace OnlineShop.Options.Models
{
    public class Option
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual ProductOption ProductOption { get; set; }
    }
}

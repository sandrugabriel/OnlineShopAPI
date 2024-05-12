using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Products.Dto
{
    public class DtoProductView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Category { get; set; }

        public DateTime Create_date { get; set; }

        public int Stock { get; set; }
    }
}

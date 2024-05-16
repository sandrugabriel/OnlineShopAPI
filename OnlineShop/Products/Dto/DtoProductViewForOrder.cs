using OnlineShop.Options.Models;

namespace OnlineShop.Products.Dto
{
    public class DtoProductViewForOrder
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Category { get; set; }

        public Option Option { get; set; }
    }
}

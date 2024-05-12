namespace OnlineShop.Products.Dto
{
    public class CreateRequestProduct
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public string Category { get; set; }

        public int Stock { get; set; }
    }
}

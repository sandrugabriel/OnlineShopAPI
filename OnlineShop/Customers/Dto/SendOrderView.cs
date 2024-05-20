namespace OnlineShop.Customers.Dto
{
    public class SendOrderView
    {
        public int CustomerId { get; set; }

        public List<ProductSendOrderView> Products { get; set; }

        public double TotalPrice { get; set; }
    }
}

namespace OnlineShop.Customers.Dto
{
    public class SendOrderRequest
    {

        public int CustomerId { get; set; }

        public List<ProductSendOrder> Products { get; set; }

    

    }
}

namespace OnlineShop.Orders.Dto
{
    public class CreateRequestOrder
    {
        public double Ammount { get; set; }

        public string OrderAddress { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }
    }
}

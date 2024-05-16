namespace OnlineShop.Orders.Dto
{
    public class UpdateRequestOrder
    {
        public double? Ammount { get; set; }

        public string? OrderAddress { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? Status { get; set; }
    }
}

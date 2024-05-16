using System.ComponentModel.DataAnnotations;

namespace OnlineShop.OrderDetails.Dto
{
    public class CreateRequestOrderDetail
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}

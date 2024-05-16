using OnlineShop.OrderDetails.Dto;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Orders.Dto
{
    public class DtoOrderView
    {
        public int Id { get; set; }

        public double Ammount { get; set; }

        public string OrderAddress { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public List<DtoOrderDetailView> Products { get; set; }
    }
}

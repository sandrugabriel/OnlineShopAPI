using OnlineShop.Orders.Dto;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Customers.Dto
{
    public class DtoCustomerView
    {

        public int Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public List<DtoOrderView> Orders { get; set; }

    }
}

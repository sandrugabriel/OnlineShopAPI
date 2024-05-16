using OnlineShop.OrderDetails.Models;
using OnlineShop.Products.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OnlineShop.Products.Dto;

namespace OnlineShop.OrderDetails.Dto
{
    public class DtoOrderDetailView
    {
        public int Id { get; set; }

        public DtoProductViewForOrder Product { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}

using OnlineShop.OrderDetails.Dto;
using OnlineShop.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.OrderDetails.Helpers
{
    public class TestOrderDetailFactory
    {
        public static DtoOrderDetailView CreateOrderDetail(int id)
        {
            DtoProductViewForOrder dtoProductViewForOrder = new DtoProductViewForOrder();
            dtoProductViewForOrder.Id = id;
            dtoProductViewForOrder.Name = id + "test";
            dtoProductViewForOrder.Price = 100;
            return new DtoOrderDetailView
            {
                Id = id,
                Price = 10,
                Quantity = 1,
                Product =dtoProductViewForOrder
            };
        }

        public static List<DtoOrderDetailView> CreateOrderDetails(int cout)
        {

            List<DtoOrderDetailView> orderDetailViews = new List<DtoOrderDetailView>();

            for (int i = 0; i < cout; i++)
            {
                orderDetailViews.Add(CreateOrderDetail(i));
            }

            return orderDetailViews;
        }
    }
}

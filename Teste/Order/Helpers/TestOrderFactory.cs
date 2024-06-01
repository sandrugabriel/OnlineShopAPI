using OnlineShop.Orders.Dto;
using OnlineShop.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.OrderDetails.Helpers;

namespace Teste.Order.Helpers
{
    public class TestOrderFactory
    {
        public static DtoOrderView CreateOrder(int id)
        {
            var factory = TestOrderDetailFactory.CreateOrderDetail(1);
            return new DtoOrderView
            {
                Id = id,
                Ammount = 10,
                Products = new List<OnlineShop.OrderDetails.Dto.DtoOrderDetailView>() { factory}
            };
        }

        public static List<DtoOrderView> CreateOrders(int cout)
        {

            List<DtoOrderView> orderViews = new List<DtoOrderView>();

            for (int i = 0; i < cout; i++)
            {
                orderViews.Add(CreateOrder(i));
            }

            return orderViews;
        }
    }
}

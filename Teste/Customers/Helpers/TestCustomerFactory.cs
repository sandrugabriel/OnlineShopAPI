using OnlineShop.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.DtoCustomerViews.Helpers
{
    public class TestCustomerFactory
    {
        public static DtoCustomerView CreateCustomer(int id)
        {
            return new DtoCustomerView
            {
                Id = id,
                Address = "test"+id,
                Country = "test",
                Email = "test" + id,
                FullName = "test" + id,
                Orders = null,
                PhoneNumber = "077777"

            };
        }

        public static List<DtoCustomerView> CreateCustomers(int cout)
        {

            List<DtoCustomerView> customers = new List<DtoCustomerView>();

            for (int i = 0; i < cout; i++)
            {
                customers.Add(CreateCustomer(i));
            }

            return customers;
        }
    }
}

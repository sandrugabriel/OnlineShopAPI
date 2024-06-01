using OnlineShop.Orders.Dto;
using OnlineShop.ProductOptions.Dto;
using OnlineShop.ProductOptions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.ProductOptions.Helpers
{
    public class TestProductOptionFactory
    {
        public static ProductOption CreateProductOption(int id)
        {
            return new ProductOption
            {
                Id = id
            };
        }

        public static List<ProductOption> CreateProductOptions(int cout)
        {

            List<ProductOption> productOptions = new List<ProductOption>();

            for (int i = 0; i < cout; i++)
            {
                productOptions.Add(CreateProductOption(i));
            }

            return productOptions;
        }
    }
}

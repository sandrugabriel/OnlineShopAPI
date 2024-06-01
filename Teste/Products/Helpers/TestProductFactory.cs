using OnlineShop.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Products.Helpers
{
    public class TestProductFactory
    {

        public static DtoProductView CreateProduct(int id)
        {
            return new DtoProductView
            {
                Id = id,
                Category = "test" + id,
                Name = "test",
                Price = 100,
                Stock = 1
            };
        }

        public static List<DtoProductView> CreateProducts(int cout)
        {

            List<DtoProductView> productViews = new List<DtoProductView>();

            for (int i = 0; i < cout; i++)
            {
                productViews.Add(CreateProduct(i));
            }

            return productViews;
        }
    }
}

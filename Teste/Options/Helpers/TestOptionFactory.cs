using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Options.Helpers
{
    public class TestOptionFactory
    {
        public static Option CreateOption(int id)
        {
            return new Option
            {
                Id = id,
                Name = "test" + id,
                Price = 100
            };
        }

        public static List<Option> CreateOptions(int cout)
        {

            List<Option> options = new List<Option>();

            for (int i = 0; i < cout; i++)
            {
                options.Add(CreateOption(i));
            }

            return options;
        }
    }
}

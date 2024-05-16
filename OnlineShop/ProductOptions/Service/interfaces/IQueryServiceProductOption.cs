using OnlineShop.Options.Models;
using OnlineShop.ProductOptions.Model;

namespace OnlineShop.ProductOptions.Service.interfaces
{
    public interface IQueryServiceProductOption
    {

        Task<List<ProductOption>> GetAllAsync();

        Task<ProductOption> GetByIdAsync(int id);
    }
}



using OnlineShop.ProductOptions.Model;

namespace OnlineShop.ProductOptions.Repository.interfaces
{
    public interface IRepositoryProductOption
    {

        Task<List<ProductOption>> GetAllAsync();

        Task<ProductOption> GetByIdAsync(int id);

    }
}

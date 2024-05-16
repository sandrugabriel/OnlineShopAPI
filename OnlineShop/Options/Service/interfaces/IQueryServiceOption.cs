using OnlineShop.Options.Models;

namespace OnlineShop.Options.Service.interfaces
{
    public interface IQueryServiceOption
    {

        Task<List<Option>> GetAllAsync();

        Task<Option> GetByIdAsync(int id);

        Task<Option> GetByNameAsync(string name);

    }
}

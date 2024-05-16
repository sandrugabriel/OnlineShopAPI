using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;

namespace OnlineShop.Options.Repository.interfaces
{
    public interface IRepositoryOption
    {
        Task<List<Option>> GetAllAsync();

        Task<Option> GetByIdAsync(int id);

        Task<Option> GetByNameAsync(string name);

        Task<Option> CreateOption(CreateRequestOption createRequest);

        Task<Option> UpdateOption(int id, UpdateRequestOption updateRequest);

        Task<Option> DeleteOption(int id);
    }
}

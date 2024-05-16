using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;

namespace OnlineShop.Options.Service.interfaces
{
    public interface ICommandServiceOption
    {
        Task<Option> CreateOption(CreateRequestOption createRequest);

        Task<Option> UpdateOption(int id, UpdateRequestOption updateRequest);

        Task<Option> DeleteOption(int id);
    }
}

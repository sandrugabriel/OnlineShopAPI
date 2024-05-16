using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;
using OnlineShop.Options.Repository.interfaces;
using OnlineShop.Options.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;

namespace OnlineShop.Options.Service
{
    public class CommandServiceOption : ICommandServiceOption
    {
        IRepositoryOption _repo;

        public CommandServiceOption(IRepositoryOption repo)
        {
            _repo = repo;
        }

        public async Task<Option> CreateOption(CreateRequestOption createRequest)
        {
            var option = await _repo.CreateOption(createRequest);

            if (option.Name.Equals("") || option.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            if (option.Price == 0)
            {
                throw new InvalidPrice(Constants.InvalidPrice);
            }


            return option;
        }

        public async Task<Option> UpdateOption(int id, UpdateRequestOption updateRequest)
        {

            var option = await _repo.UpdateOption(id, updateRequest);

            if (option == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (option.Name.Equals("") || option.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            if (updateRequest.Price == 0)
            {
                throw new InvalidPrice(Constants.InvalidPrice);
            }

            return option;
        }

        public async Task<Option> DeleteOption(int id)
        {
            var option = await _repo.DeleteOption(id);

            if (option == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return option;
        }

    }
}

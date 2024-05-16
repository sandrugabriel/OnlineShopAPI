using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;
using OnlineShop.Options.Repository.interfaces;
using OnlineShop.Options.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;

namespace OnlineShop.Options.Service
{
    public class QueryServiceOption : IQueryServiceOption
    {

        IRepositoryOption _repo;

        public QueryServiceOption(IRepositoryOption repo)
        {
            _repo = repo;
        }

        public async Task<List<Option>> GetAllAsync()
        {
            var option = await _repo.GetAllAsync();
            if (option == null)
            {
                throw new ItemsDoNotExist(Constants.ItemsDoNotExist);
            }

            return option;
        }

        public async Task<Option> GetByIdAsync(int id)
        {
            var option = await _repo.GetByIdAsync(id);
            if (option == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return option;
        }

        public async Task<Option> GetByNameAsync(string name)
        {
            var option = await _repo.GetByNameAsync(name);
            if (option == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return option;
        }
    }
}

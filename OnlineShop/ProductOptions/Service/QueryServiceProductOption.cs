
using OnlineShop.ProductOptions.Model;
using OnlineShop.ProductOptions.Repository.interfaces;
using OnlineShop.ProductOptions.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;

namespace OnlineShop.ProductProductOptions.Service
{
    public class QueryServiceProductOption : IQueryServiceProductOption
    {
        IRepositoryProductOption _repo;

        public QueryServiceProductOption(IRepositoryProductOption repo)
        {
            _repo = repo;
        }

        public async Task<List<ProductOption>> GetAllAsync()
        {
            var option = await _repo.GetAllAsync();
            if (option == null)
            {
                throw new ItemsDoNotExist(Constants.ItemsDoNotExist);
            }

            return option;
        }

        public async Task<ProductOption> GetByIdAsync(int id)
        {
            var option = await _repo.GetByIdAsync(id);
            if (option == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return option;
        }

    }
}

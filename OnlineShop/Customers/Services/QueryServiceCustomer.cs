using OnlineShop.Customers.Dto;
using OnlineShop.Customers.Repository.interfaces;
using OnlineShop.Customers.Services.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;

namespace OnlineShop.Customers.Services
{
    public class QueryServiceCustomer : IQueryServiceCustomer
    {
        IRepositoryCustomer _repo;

        public QueryServiceCustomer(IRepositoryCustomer repo)
        {
            _repo = repo;
        }

        public async Task<List<DtoCustomerView>> GetAllAsync()
        {
            var customer = await _repo.GetAllAsync();
            if(customer == null)
            {
                throw new ItemsDoNotExist(Constants.ItemsDoNotExist);
            }

            return customer;
        }

        public async Task<DtoCustomerView> GetById(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return customer;
        }

        public async Task<DtoCustomerView> GetByName(string name)
        {
            var customer = await _repo.GetByNameAsync(name);
            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return customer;
        }
    }
}

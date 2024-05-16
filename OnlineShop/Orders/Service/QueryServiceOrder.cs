using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Dto;
using OnlineShop.Orders.Repository.interfaces;
using OnlineShop.Orders.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;

namespace OnlineShop.Orders.Service
{
    public class QueryServiceOrder : IQueryServiceOrder
    {
        IRepositoryOrder _repo;

        public QueryServiceOrder(IRepositoryOrder repo)
        {
            _repo = repo;
        }

        public async Task<List<DtoOrderView>> GetAllAsync()
        {
           var orders = await _repo.GetAllAsync();

            if (orders == null)
            {
                throw new ItemsDoNotExist(Constants.ItemsDoNotExist);
            }

            return orders;
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _repo.GetById(id);

            if (order == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return order;
        }

        public async Task<DtoOrderView> GetByIdAsync(int id)
        {
            var order = await _repo.GetByIdAsync(id);

            if (order == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return order;
        }
    }
}

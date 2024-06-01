using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;
using OnlineShop.OrderDetails.Repository.interfaces;
using OnlineShop.OrderDetails.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;

namespace OnlineShop.OrderDetailDetails.Service
{
    public class QueryServiceOrderDetail : IQueryServiceOrderDetail
    {
        IRepositoryOrderDetail _repo;

        public QueryServiceOrderDetail(IRepositoryOrderDetail repo)
        {
            _repo = repo;
        }

        public async Task<List<DtoOrderDetailView>> GetAllAsync()
        {
            var orderDetails = await _repo.GetAllAsync();

            if (orderDetails == null)
            {
                throw new ItemsDoNotExist(Constants.ItemsDoNotExist);
            }

            return orderDetails;
        }
        public async Task<DtoOrderDetailView> GetByIdAsync(int id)
        {
            var orderDetail = await _repo.GetByIdAsync(id);

            if (orderDetail == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return orderDetail;
        }

        public async Task<OrderDetail> GetById(int id)
        {
            var orderDetail = await _repo.GetById(id);

            if (orderDetail == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
                return orderDetail;
        }
    }
}

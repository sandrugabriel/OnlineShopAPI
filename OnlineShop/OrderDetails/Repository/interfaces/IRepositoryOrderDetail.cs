using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;

namespace OnlineShop.OrderDetails.Repository.interfaces
{
    public interface IRepositoryOrderDetail
    {
        Task<List<DtoOrderDetailView>> GetAllAsync();

        Task<DtoOrderDetailView> GetByIdAsync(int id);

        Task<OrderDetail> GetById(int id);

        void SaveOrderDetails(List<OrderDetail> orderDetail);

    }
}

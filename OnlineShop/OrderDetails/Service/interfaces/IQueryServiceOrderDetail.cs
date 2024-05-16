using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;

namespace OnlineShop.OrderDetails.Service.interfaces
{
    public interface IQueryServiceOrderDetail
    {

        Task<List<DtoOrderDetailView>> GetAllAsync();

        Task<DtoOrderDetailView> GetByIdAsync(int id);

        Task<OrderDetail> GetById(int id);

    }
}

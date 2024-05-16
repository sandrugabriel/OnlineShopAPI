using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Dto;

namespace OnlineShop.Orders.Service.interfaces
{
    public interface IQueryServiceOrder
    {

        Task<List<DtoOrderView>> GetAllAsync();

        Task<DtoOrderView> GetByIdAsync(int id);

        Task<Order> GetById(int id);

    }
}

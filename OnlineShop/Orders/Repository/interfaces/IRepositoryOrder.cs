using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Dto;

namespace OnlineShop.Orders.Repository.interfaces
{
    public interface IRepositoryOrder
    {

        Task<List<DtoOrderView>> GetAllAsync();

        Task<DtoOrderView> GetByIdAsync(int id);

        Task<Order> GetById(int id);

        Task SaveOrder(Order order);


    }
}

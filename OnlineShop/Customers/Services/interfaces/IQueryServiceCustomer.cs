using OnlineShop.Customers.Dto;

namespace OnlineShop.Customers.Services.interfaces
{
    public interface IQueryServiceCustomer
    {

        Task<List<DtoCustomerView>> GetAllAsync();

        Task<DtoCustomerView> GetById(int id);

        Task<DtoCustomerView> GetByName(string name);

    }
}

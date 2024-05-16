using OnlineShop.Customers.Dto;

namespace OnlineShop.Customers.Services.interfaces
{
    public interface ICommandServiceCustomer
    {

        Task<DtoCustomerView> CreateCustomer(CreateRequestCustomer createRequest);

        Task<DtoCustomerView> UpdateCustomer(int id, UpdateRequestCustomer updateRequest);

        Task<DtoCustomerView> DeleteCustomer(int id);

        Task<DtoCustomerView> AddProductToOrder(int idCurtomer, string name, string option, int quantity);

        Task<DtoCustomerView> DeleteOrder(int idCustomer, int idOrder);

        Task<DtoCustomerView> DeleteProductToOrder(int idCurtomer, string name, string option);

    }
}


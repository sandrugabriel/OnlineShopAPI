using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;

namespace OnlineShop.Products.Service.interfaces
{
    public interface IQueryServiceProduct
    {

        Task<List<DtoProductView>> GetAllAsync();

        Task<Product> GetById(int id);

        Task<DtoProductView> GetByIdAsync(int id);

        Task<DtoProductView> GetByNameAsync(string name);

        Task<Product> GetByName(string name);

    }
}

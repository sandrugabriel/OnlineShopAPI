using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;

namespace OnlineShop.Products.Repository.interfaces
{
    public interface IRepositoryProduct
    {

        Task<List<DtoProductView>> GetAllAsync();

        Task<Product> GetById(int id);
            
        Task<DtoProductView> GetByIdAsync(int id);

        Task<DtoProductView> GetByNameAsync(string name);

        Task<Product> GetByName(string name);

        Task<DtoProductView> Create(CreateRequestProduct request);

        Task<DtoProductView> UpdateAsync(int id, UpdateRequestProduct request);

        Task<DtoProductView> DeleteById(int id);

        Task<DtoProductView> AddOption(int id, string name);

        Task<DtoProductView> DeleteOption(int id, string name);


    }
}

using OnlineShop.Products.Dto;

namespace OnlineShop.Products.Service.interfaces
{
    public interface ICommandServiceProduct
    {

        Task<DtoProductView> Create(CreateRequestProduct request);

        Task<DtoProductView> Update(int id, UpdateRequestProduct request);

        Task<DtoProductView> Delete(int id);

    }
}

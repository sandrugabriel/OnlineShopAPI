
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;
using OnlineShop.Products.Repository.interfaces;
using OnlineShop.Products.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;

namespace OnlineShop.Products.Service
{
    public class CommandServiceProduct : ICommandServiceProduct
    {

        private IRepositoryProduct _repository;

        public CommandServiceProduct(IRepositoryProduct repository)
        {
            _repository = repository;
        }

        public async Task<DtoProductView> Create(CreateRequestProduct request)
        {

            if (request.Price <= 0)
            {
                throw new InvalidPrice(Constants.InvalidPrice);
            }
            var product = await _repository.Create(request);

            return product;
        }

        public async Task<DtoProductView> Update(int id, UpdateRequestProduct request)
        {

            var product = await _repository.GetById(id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }


            if (request.Price <= 0)
            {
                throw new InvalidPrice(Constants.InvalidPrice);
            }

            DtoProductView dtoProductView = new DtoProductView();
            dtoProductView.Id = product.Id;
            dtoProductView.Name = product.Name;
            dtoProductView.Price = product.Price;
            dtoProductView.Create_date = product.Create_date;
            dtoProductView.Category = product.Category;
            dtoProductView.Stock = product.Stock;

            dtoProductView = await _repository.UpdateAsync(id, request);

            return dtoProductView;
        }

        public async Task<DtoProductView> Delete(int id)
        {

            var product = await _repository.GetById(id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            await _repository.DeleteById(id);


            DtoProductView dtoProductView = new DtoProductView();
            dtoProductView.Id = product.Id;
            dtoProductView.Name = product.Name;
            dtoProductView.Price = product.Price;
            dtoProductView.Create_date = product.Create_date;
            dtoProductView.Category = product.Category;
            dtoProductView.Stock = product.Stock;

            return dtoProductView;
        }

    }
}

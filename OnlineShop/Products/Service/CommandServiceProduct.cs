
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;
using OnlineShop.Products.Repository.interfaces;
using OnlineShop.Products.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;
using System.Data.Entity.Core.Metadata.Edm;

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

            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }


            if (request.Price <= 0)
            {
                throw new InvalidPrice(Constants.InvalidPrice);
            }


             await _repository.UpdateAsync(id, request);

            return product;
        }

        public async Task<DtoProductView> Delete(int id)
        {
            
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            await _repository.DeleteById(id);

            return product;
        }

        public async Task<DtoProductView> AddOption(int id, string name)
        {
            var p = await _repository.GetById(id);
            if(p == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            var product = await _repository.AddOption(id, name);
            if (product == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            return product;
        }

        public async Task<DtoProductView> DeleteOption(int id, string name)
        {

            var product = await _repository.GetById(id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            var dto =  await _repository.DeleteOption(id,name);

            if(dto == null)
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return dto;
        }
    }
}

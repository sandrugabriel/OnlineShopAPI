
using OnlineShop.Options.Models;
using OnlineShop.Options.Repository.interfaces;
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
        private IRepositoryOption _repoOption;

        public CommandServiceProduct(IRepositoryProduct repository, IRepositoryOption option)
        {
            _repository = repository;
            _repoOption = option;
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
            var p = await _repository.GetByIdAsync(id);
            if(p == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

           
            var option = await _repoOption.GetByNameAsync(name);
            if(option == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            p = await _repository.AddOption(id,option);

            return p;
        }

        public async Task<DtoProductView> DeleteOption(int id, string name)
        {

            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            var option =  await _repoOption.GetByNameAsync(name);

            if(option == null)
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            await _repository.DeleteOption(id,name);

            return product;
        }
    }
}

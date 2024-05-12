using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;
using OnlineShop.Products.Repository.interfaces;
using OnlineShop.Products.Service.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;

namespace OnlineShop.Products.Service
{
    public class QueryServiceProduct : IQueryServiceProduct
    {

        private IRepositoryProduct _repository;

        public QueryServiceProduct(IRepositoryProduct repository)
        {
            _repository = repository;
        }

        public async Task<List<DtoProductView>> GetAllAsync()
        {
            var student = await _repository.GetAllAsync();

            if (student.Count() == 0)
            {
                throw new ItemsDoNotExist(Constants.ItemsDoNotExist);
            }

            return student;
        }

        public async Task<DtoProductView> GetByNameAsync(string name)
        {
            var student = await _repository.GetByNameAsync(name);

            if (student == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            return student;
        }

        public async Task<Product> GetByName(string name)
        {
            var student = await _repository.GetByName(name);

            if (student == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            return student;
        }

        public async Task<DtoProductView> GetByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return student;
        }

        public async Task<Product> GetById(int id)
        {
            var student = await _repository.GetById(id);

            if (student == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return student;
        }
    }
}

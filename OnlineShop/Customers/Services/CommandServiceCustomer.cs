using OnlineShop.Customers.Dto;
using OnlineShop.Customers.Models;
using OnlineShop.Customers.Repository.interfaces;
using OnlineShop.Customers.Services.interfaces;
using OnlineShop.OrderDetails.Models;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace OnlineShop.Customers.Services
{
    public class CommandServiceCustomer : ICommandServiceCustomer
    {
        IRepositoryCustomer _repo;

        public CommandServiceCustomer(IRepositoryCustomer repo)
        {
            _repo = repo;
        }

        public async Task<DtoCustomerView> CreateCustomer(CreateRequestCustomer createRequest)
        {
           var customer = await _repo.CreateCustomer(createRequest);

            if(customer.FullName.Equals("") || customer.FullName.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            return customer;
        }

        public async Task<DtoCustomerView> UpdateCustomer(int id, UpdateRequestCustomer updateRequest)
        {

            var customer = await _repo.UpdateCustomer(id,updateRequest);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (customer.FullName.Equals("") || customer.FullName.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            return customer;
        }

        public async Task<DtoCustomerView> DeleteCustomer(int id)
        {
            var customer = await _repo.DeleteCustomer(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return customer;
        }

        public async Task<DtoCustomerView> AddProductToOrder(int idCurtomer, string name, string option, int quantity)
        {

            var customer = await _repo.AddProductToOrder(idCurtomer,name,option,quantity);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (name.Equals("") || option.Equals(""))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            return customer;
        }

        public async Task<DtoCustomerView> DeleteOrder(int idCustomer, int idOrder)
        {
            var customer = await _repo.DeleteOrder(idCustomer,idOrder);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return customer;
        }

        public async Task<DtoCustomerView> DeleteProductToOrder(int idCurtomer, string name, string option)
        {
            var customer = await _repo.DeleteProductToOrder(idCurtomer, name,option);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            return customer;

        }
    }
}

using OnlineShop.Customers.Dto;
using OnlineShop.Customers.Models;
using OnlineShop.Customers.Repository.interfaces;
using OnlineShop.Customers.Services.interfaces;
using OnlineShop.OrderDetails.Models;
using OnlineShop.OrderDetails.Repository.interfaces;
using OnlineShop.Orders.Repository.interfaces;
using OnlineShop.Products.Models;
using OnlineShop.Products.Repository.interfaces;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace OnlineShop.Customers.Services
{
    public class CommandServiceCustomer : ICommandServiceCustomer
    {
        IRepositoryCustomer _repo;
        IRepositoryOrder _repoOrder;
        IRepositoryProduct _repoProduct;
        IRepositoryOrderDetail _repoOrderDetail;

        public CommandServiceCustomer(IRepositoryCustomer repo,IRepositoryOrder repositoryOrder,IRepositoryProduct product,IRepositoryOrderDetail orderDetail)
        {
            _repo = repo;
            _repoOrder = repositoryOrder;
            _repoProduct = product;
            _repoOrderDetail = orderDetail;
        }

        public async Task<DtoCustomerView> CreateCustomer(CreateRequestCustomer createRequest)
        {
            if(createRequest.FullName.Equals("") || createRequest.FullName.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

           var customer = await _repo.CreateCustomer(createRequest);

            return customer;
        }

        public async Task<DtoCustomerView> UpdateCustomer(int id, UpdateRequestCustomer updateRequest)
        {

            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (customer.FullName.Equals("") || customer.FullName.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            customer = await _repo.UpdateCustomer(id,updateRequest);
            return customer;
        }

        public async Task<DtoCustomerView> DeleteCustomer(int id)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            await _repo.DeleteCustomer(id);

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
            var customer = await _repo.GetByIdAsync(idCustomer);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            var order = await _repoOrder.GetByIdAsync(idOrder);
            if(order == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            await _repo.DeleteOrder(idCustomer, idOrder);
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


        public async Task<SendOrderView> SaveOrder(SendOrderRequest orderRequest)
        {

            SendOrderView sendOrderView = new SendOrderView();
           Order order = new Order();
            order.CustomerId = orderRequest.CustomerId;

            var customer = await _repo.GetById(orderRequest.CustomerId);

            if(customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            
            order.Customer = customer;
            order.OrderDate = DateTime.Now;
            order.OrderAddress = customer.Address;

            List<OrderDetail> orderDetails = new List<OrderDetail>();

            List<ProductSendOrderView> productSendOrderViews = new List<ProductSendOrderView>();

            foreach (ProductSendOrder dtoProduct in orderRequest.Products)
            {
                ProductSendOrderView productSendOrderView = new ProductSendOrderView();
                var product = await _repoProduct.GetByName(dtoProduct.NameProduct);

                OrderDetail orderDetail = new OrderDetail();
                orderDetail.Product = product;
                orderDetail.ProductId = product.Id;
                orderDetail.Order = order;
                orderDetail.OrderId = order.Id;
                orderDetail.Price = product.Price * dtoProduct.Quantity;
                orderDetail.Quantity = dtoProduct.Quantity;

                orderDetails.Add(orderDetail);
                order.Ammount += orderDetail.Price;
                productSendOrderView.Price = product.Price;
                productSendOrderView.Quantity = dtoProduct.Quantity;
                productSendOrderView.NameProduct = product.Name;
                productSendOrderViews.Add(productSendOrderView);



            }
            order.OrderDetails = orderDetails;
            order.Status = "close";
            _repoOrder.SaveOrder(order);
       

           

            sendOrderView.TotalPrice = order.Ammount;

            sendOrderView.CustomerId = orderRequest.CustomerId;
            sendOrderView.Products = productSendOrderViews;

            return sendOrderView;
        }
    }
}

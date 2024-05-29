using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Customers.Dto;
using OnlineShop.Customers.Models;
using OnlineShop.Customers.Repository.interfaces;
using OnlineShop.Data;
using OnlineShop.Options.Models;
using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Dto;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;
using OnlineShop.System.Constants;
using OnlineShop.System.Exceptions;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Xml.Linq;

namespace OnlineShop.Customers.Repository
{
    public class RepositoryCustomers : IRepositoryCustomer
    {
        AppDbContext _context;
        IMapper _mapper;

        public RepositoryCustomers(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<DtoCustomerView> CreateCustomer(CreateRequestCustomer createRequest)
        {
           
            var customer = _mapper.Map<Customer>(createRequest);

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            DtoCustomerView customerView = _mapper.Map<DtoCustomerView>(customer);

            return customerView;
        }

        public async Task<DtoCustomerView> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return null;

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

          
            return _mapper.Map<DtoCustomerView>(customer);
        }


        public async Task<List<DtoCustomerView>> GetAllAsync()
        {
            List<Customer> customers = await _context.Customers.Include(s=>s.Orders).
                ThenInclude(s=>s.OrderDetails).ThenInclude(s=>s.Product).ToListAsync();

            List<DtoCustomerView> dtoCustomerViews = _mapper.Map<List<DtoCustomerView>>(customers);

            return dtoCustomerViews;
        }

        public async Task<DtoCustomerView> GetByIdAsync(int id)
        {

            var customer = await _context.Customers.Include(s => s.Orders).
                ThenInclude(s => s.OrderDetails).ThenInclude(s => s.Product).FirstOrDefaultAsync(s=>s.Id==id);

            return _mapper.Map<DtoCustomerView>(customer);
        }

        public async Task<DtoCustomerView> GetByNameAsync(string name)
        {
            var customer = await _context.Customers.Include(s => s.Orders).
                 ThenInclude(s => s.OrderDetails).ThenInclude(s => s.Product).FirstOrDefaultAsync(s => s.FullName == name);

            return _mapper.Map<DtoCustomerView>(customer);
        }

        public async Task<DtoCustomerView> UpdateCustomer(int id,UpdateRequestCustomer updateRequest)
        {
            var customer = await _context.Customers.Include(s => s.Orders).
               ThenInclude(s => s.OrderDetails).ThenInclude(s => s.Product).FirstOrDefaultAsync(s => s.Id == id);
            customer.Address = updateRequest.Address ?? customer.Address;
            customer.PhoneNumber = updateRequest.PhoneNumber ?? customer.PhoneNumber;
            customer.FullName = updateRequest.FullName ?? customer.FullName;
            customer.Country = updateRequest.Country ?? customer.Country;
            customer.Email = updateRequest.Email ?? customer.Email;

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();

            DtoCustomerView customerView = _mapper.Map<DtoCustomerView>(customer);

            return customerView;
        }

        public async Task<DtoCustomerView> AddProductToOrder(int idCurtomer, string name, string option,int quantity)
        {
            var customer = await _context.Customers.FindAsync(idCurtomer);

                       var order = (Order)null;
            if(customer.Orders !=null)
            order = customer.Orders.FirstOrDefault(s=>s.Status == "open");

            Product product = _context.Products.Include(s => s.OrderDetails).Include(s => s.ProductOptions).FirstOrDefault(s=>s.Name == name);

            if (product == null) return null;

            Option option1 = null;
            foreach (var op in product.ProductOptions)
            {
                if (_context.Options.FirstOrDefault(s => s.Id == op.IdOption).Name == option) option1 = op.Option;
            }

            if (option1 == null) return null;

            if (product.Stock < quantity) throw new InvalidQuantity(Constants.InvalidQuantity);

            OrderDetail orderDetail = new OrderDetail();
            orderDetail.Product = product;
            orderDetail.ProductId = product.Id;
            orderDetail.Quantity = quantity;
            orderDetail.Price = quantity * product.Price + option1.Price;
            _context.OrderDetails.Add(orderDetail);

            await _context.SaveChangesAsync();

            Order finalOrder = null;
            if(order == null)
            {
               finalOrder = new Order();
                finalOrder.Status = "open";
                finalOrder.OrderDate = DateTime.Now;
                finalOrder.OrderAddress = customer.Address;
                finalOrder.Customer = customer;
                finalOrder.CustomerId = customer.Id;
                finalOrder.OrderDetails = new List<OrderDetail>();
                _context.Orders.Add(finalOrder);
                await _context.SaveChangesAsync();
            }
            else
            {
                finalOrder = order;
                finalOrder.Id = order.Id;
                finalOrder.OrderDate = DateTime.Now;
                finalOrder.OrderAddress = customer.Address;
                finalOrder.Customer = customer;
                finalOrder.CustomerId = customer.Id;
                finalOrder.OrderDetails = order.OrderDetails;
            }

            finalOrder.Ammount += orderDetail.Price;
            orderDetail.Order = finalOrder;
            orderDetail.OrderId = finalOrder.Id;

            product.Stock -= quantity;

            _context.Products.Update(product);

            _context.Orders.Update(finalOrder);

            await _context.SaveChangesAsync();


            return _mapper.Map<DtoCustomerView>(customer);
        }

        public async Task<DtoCustomerView> DeleteOrder(int idCustomer, int idOrder)
        {
            var customer = await _context.Customers.Include(s => s.Orders).
                 ThenInclude(s => s.OrderDetails).ThenInclude(s => s.Product).FirstOrDefaultAsync(s => s.Id == idCustomer);

          
            customer.Orders.Remove(customer.Orders.FirstOrDefault(or=>or.Id==idOrder));

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<DtoCustomerView>(customer);
        }

        public async Task<DtoCustomerView> DeleteProductToOrder(int idCurtomer, string name, string option)
        {
            var customer = await _context.Customers.Include(s => s.Orders).
                  ThenInclude(s => s.OrderDetails).ThenInclude(s => s.Product).FirstOrDefaultAsync(s => s.Id == idCurtomer);

            var order = _context.Orders.Include(s => s.OrderDetails).FirstOrDefault(s => s.CustomerId == idCurtomer && s.Status == "open");

            if (order == null) return null;

            var orderDetail = (OrderDetail)null;
            Product product = null;
            foreach (var item in order.OrderDetails)
            {
                if (_context.Products.FirstOrDefaultAsync(s => s.Id == item.ProductId).Result.Name == name) { orderDetail = item; product = item.Product; }
            }

            if (orderDetail == null) return null;

            product.Stock += orderDetail.Quantity;

            _context.Products.Update(product);

            order.Ammount -= orderDetail.Price;

            _context.OrderDetails.Remove(orderDetail);

            _context.Orders.Update(order);

            await _context.SaveChangesAsync();



            return _mapper.Map<DtoCustomerView>(customer);    
        }

        public async Task<Customer> GetById(int id)
        {

            List<Customer> customers = await _context.Customers.ToListAsync();

            foreach (Customer customer1 in customers)
            {
                if (customer1.Id == id) return customer1;
            }

            return null;
        }

    }
}

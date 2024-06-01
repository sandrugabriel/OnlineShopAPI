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
using OnlineShop.ProductOptions.Model;
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
            var customer = await _context.Customers.Include(s => s.Orders).
                ThenInclude(s => s.OrderDetails).ThenInclude(s => s.Product).FirstOrDefaultAsync(s => s.Id == idCurtomer);


            var order = (Order)null;
            if(customer.Orders !=null)
            order = customer.Orders.FirstOrDefault(s=>s.Status == "open");

            OrderDetail orderDetail = order.OrderDetails.FirstOrDefault(s=>s.Product.Name == name);

            if (orderDetail == null) return null;

            if (orderDetail.Product.Stock < quantity) throw new InvalidQuantity(Constants.InvalidQuantity);

            ProductOption option1 = orderDetail.Product.ProductOptions.FirstOrDefault(s => s.Option.Name == option);

            OrderDetail finalOrderDetail = new OrderDetail();
            finalOrderDetail.Product = orderDetail.Product;
            finalOrderDetail.ProductId = orderDetail.Product.Id;
            finalOrderDetail.Quantity = quantity;
            finalOrderDetail.Price = quantity * orderDetail.Product.Price + option1.Option.Price;


            Order finalOrder = new Order();
                finalOrder.Status = "open";
                finalOrder.OrderDate = DateTime.Now;
                finalOrder.OrderAddress = customer.Address;
                finalOrder.Customer = customer;
                finalOrder.CustomerId = customer.Id;
                finalOrder.OrderDetails = new List<OrderDetail>();
            if(order != null)
            {
               order.OrderDetails.Add(finalOrderDetail);
                finalOrder = order;
                finalOrder.OrderDate = DateTime.Now;
                finalOrder.OrderAddress = customer.Address;
                finalOrder.Customer = customer;
                finalOrder.CustomerId = customer.Id;
            }

            finalOrder.Ammount += orderDetail.Price;
            orderDetail.Order = finalOrder;
            orderDetail.OrderId = finalOrder.Id;

           orderDetail.Product.Stock -= quantity;

            _context.Customers.Update(customer);

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

            var order = customer.Orders.FirstOrDefault(s=>s.Status == "open");

            if (order == null) return null;

            var orderDetail = (OrderDetail)null;
            Product product = null;
            foreach (var item in order.OrderDetails)
            {
                if (item.Product.Name == name) { orderDetail = item; product = item.Product; }
            }

            product.Stock += orderDetail.Quantity;

            order.Ammount -= orderDetail.Price;

            order.OrderDetails.Remove(orderDetail);

            _context.Customers.Update(customer);

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

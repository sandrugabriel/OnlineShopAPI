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

            DtoCustomerView customerView = new DtoCustomerView();
            customerView.Id = customer.Id;
            customerView.Address = customer.Address;
            customerView.PhoneNumber = customer.PhoneNumber;
            customerView.FullName = customer.FullName;
            customerView.Country = customer.Country;
            customerView.Email = customer.Email;

            return customerView;
        }

        public async Task<DtoCustomerView> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return null;

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            DtoCustomerView customerView = new DtoCustomerView();
            customerView.Id = customer.Id;
            customerView.Address = customer.Address;
            customerView.PhoneNumber = customer.PhoneNumber;
            customerView.FullName = customer.FullName;
            customerView.Country = customer.Country;
            customerView.Email = customer.Email;
            List<Order> orders = await _context.Orders.Include(s => s.OrderDetails).Where(s => s.CustomerId == customer.Id).ToListAsync();

            List<DtoOrderView> orderViews = new List<DtoOrderView>();

            foreach (Order order in orders)
            {

                DtoOrderView dtoOrderView = new DtoOrderView();
                dtoOrderView.Id = order.Id;
                dtoOrderView.Ammount = order.Ammount;
                dtoOrderView.OrderDate = order.OrderDate;
                dtoOrderView.OrderAddress = order.OrderAddress;
                dtoOrderView.Status = order.Status;

                List<DtoOrderDetailView> dtoODetailViews = new List<DtoOrderDetailView>();

                foreach (OrderDetail orderDetail in order.OrderDetails)
                {
                    DtoOrderDetailView dto = new DtoOrderDetailView();

                    dto.Id = orderDetail.Id;
                    dto.Price = orderDetail.Price;
                    dto.Quantity = orderDetail.Quantity;
                    DtoProductViewForOrder dtoProductView = new DtoProductViewForOrder();
                    Product product = await _context.Products.FindAsync(orderDetail.ProductId);
                    dtoProductView.Id = product.Id;
                    dtoProductView.Price = product.Price;
                    dtoProductView.Name = product.Name;
                    dtoProductView.Category = product.Category;
                  //  dtoProductView.Option = await _context.Options.FirstOrDefaultAsync(s => s.Name == name);


                    dto.Product = dtoProductView;

                    dtoODetailViews.Add(dto);
                }

                dtoOrderView.Products = dtoODetailViews;

                orderViews.Add(dtoOrderView);
            }


            customerView.Orders = orderViews;


            return customerView;
        }


        public async Task<List<DtoCustomerView>> GetAllAsync()
        {
            List<Customer> customers = await _context.Customers.Include(s=>s.Orders).ToListAsync();

            List<DtoCustomerView> customerViews = new List<DtoCustomerView>();

            foreach (Customer customer in customers)
            {
                DtoCustomerView customerView = new DtoCustomerView();
                customerView.Id = customer.Id;
                customerView.Address = customer.Address;
                customerView.PhoneNumber = customer.PhoneNumber;
                customerView.FullName = customer.FullName;
                customerView.Country = customer.Country;
                customerView.Email = customer.Email;
                List<Order> orders = await _context.Orders.Include(s => s.OrderDetails).Where(s=>s.CustomerId == customer.Id).ToListAsync();

                List<DtoOrderView> orderViews = new List<DtoOrderView>();

                foreach (Order order in orders)
                {
                    
                    DtoOrderView dtoOrderView = new DtoOrderView();
                    dtoOrderView.Id = order.Id;
                    dtoOrderView.Ammount = order.Ammount;
                    dtoOrderView.OrderDate = order.OrderDate;
                    dtoOrderView.OrderAddress = order.OrderAddress;
                    dtoOrderView.Status = order.Status;

                    List<DtoOrderDetailView> dtoODetailViews = new List<DtoOrderDetailView>();

                    foreach (OrderDetail orderDetail in order.OrderDetails)
                    {
                        DtoOrderDetailView dto = new DtoOrderDetailView();

                        dto.Id = orderDetail.Id;
                        dto.Price = orderDetail.Price;
                        dto.Quantity = orderDetail.Quantity;
                        DtoProductViewForOrder dtoProductView = new DtoProductViewForOrder();
                        Product product = await _context.Products.FindAsync(orderDetail.ProductId);
                        dtoProductView.Id = product.Id;
                        dtoProductView.Price = product.Price;
                        dtoProductView.Name = product.Name;
                        dtoProductView.Category = product.Category;
                       // dtoProductView.Option = await _context.Options.FirstOrDefaultAsync(s => s.Name == name);


                        dto.Product = dtoProductView;

                        dtoODetailViews.Add(dto);
                    }

                    dtoOrderView.Products = dtoODetailViews;

                    orderViews.Add(dtoOrderView);
                }


                customerView.Orders = orderViews;

                customerViews.Add(customerView);
            }

            return customerViews;
        }

        public async Task<DtoCustomerView> GetByIdAsync(int id)
        {
            List<Customer> customers = await _context.Customers.ToListAsync();

            var customer = (Customer)null;
            foreach (Customer customer1 in customers)
            {
                if(customer1.Id == id) customer = customer1;
            }

            DtoCustomerView customerView = new DtoCustomerView();
            customerView.Id = customer.Id;
            customerView.Address = customer.Address;
            customerView.PhoneNumber = customer.PhoneNumber;
            customerView.FullName = customer.FullName;
            customerView.Country = customer.Country;
            customerView.Email = customer.Email;
            List<Order> orders = await _context.Orders.Include(s => s.OrderDetails).Where(s => s.CustomerId == customer.Id).ToListAsync();

            List<DtoOrderView> orderViews = new List<DtoOrderView>();

            foreach (Order order in orders)
            {

                DtoOrderView dtoOrderView = new DtoOrderView();
                dtoOrderView.Id = order.Id;
                dtoOrderView.Ammount = order.Ammount;
                dtoOrderView.OrderDate = order.OrderDate;
                dtoOrderView.OrderAddress = order.OrderAddress;
                dtoOrderView.Status = order.Status;

                List<DtoOrderDetailView> dtoODetailViews = new List<DtoOrderDetailView>();

                foreach (OrderDetail orderDetail in order.OrderDetails)
                {
                    DtoOrderDetailView dto = new DtoOrderDetailView();

                    dto.Id = orderDetail.Id;
                    dto.Price = orderDetail.Price;
                    dto.Quantity = orderDetail.Quantity;
                    DtoProductViewForOrder dtoProductView = new DtoProductViewForOrder();
                    Product product = await _context.Products.FindAsync(orderDetail.ProductId);
                    dtoProductView.Id = product.Id;
                    dtoProductView.Price = product.Price;
                    dtoProductView.Name = product.Name;
                    dtoProductView.Category = product.Category;
                    //dtoProductView.Option = await _context.Options.FirstOrDefaultAsync(s => s.Name == name);


                    dto.Product = dtoProductView;

                    dtoODetailViews.Add(dto);
                }

                dtoOrderView.Products = dtoODetailViews;

                orderViews.Add(dtoOrderView);
            }


            customerView.Orders = orderViews;


            return customerView;
        }

        public async Task<DtoCustomerView> GetByNameAsync(string name)
        {
            List<Customer> customers = await _context.Customers.ToListAsync();

            var customer = (Customer)null;
            foreach (Customer customer1 in customers)
            {
                if (customer1.FullName.Equals(name)) customer = customer1;
            }

            DtoCustomerView customerView = new DtoCustomerView();
            customerView.Id = customer.Id;
            customerView.Address = customer.Address;
            customerView.PhoneNumber = customer.PhoneNumber;
            customerView.FullName = customer.FullName;
            customerView.Country = customer.Country;
            customerView.Email = customer.Email;
            List<Order> orders = await _context.Orders.Include(s => s.OrderDetails).Where(s => s.CustomerId == customer.Id).ToListAsync();

            List<DtoOrderView> orderViews = new List<DtoOrderView>();

            foreach (Order order in orders)
            {

                DtoOrderView dtoOrderView = new DtoOrderView();
                dtoOrderView.Id = order.Id;
                dtoOrderView.Ammount = order.Ammount;
                dtoOrderView.OrderDate = order.OrderDate;
                dtoOrderView.OrderAddress = order.OrderAddress;
                dtoOrderView.Status = order.Status;

                List<DtoOrderDetailView> dtoODetailViews = new List<DtoOrderDetailView>();

                foreach (OrderDetail orderDetail in order.OrderDetails)
                {
                    DtoOrderDetailView dto = new DtoOrderDetailView();

                    dto.Id = orderDetail.Id;
                    dto.Price = orderDetail.Price;
                    dto.Quantity = orderDetail.Quantity;
                    DtoProductViewForOrder dtoProductView = new DtoProductViewForOrder();
                    Product product = await _context.Products.FindAsync(orderDetail.ProductId);
                    dtoProductView.Id = product.Id;
                    dtoProductView.Price = product.Price;
                    dtoProductView.Name = product.Name;
                    dtoProductView.Category = product.Category;
                    dtoProductView.Option = await _context.Options.FirstOrDefaultAsync(s => s.Name == name);


                    dto.Product = dtoProductView;

                    dtoODetailViews.Add(dto);
                }

                dtoOrderView.Products = dtoODetailViews;

                orderViews.Add(dtoOrderView);
            }


            customerView.Orders = orderViews;


            return customerView;
        }

        public async Task<DtoCustomerView> UpdateCustomer(int id,UpdateRequestCustomer updateRequest)
        {
           var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return null;

            customer.Address = updateRequest.Address ?? customer.Address;
            customer.PhoneNumber = updateRequest.PhoneNumber ?? customer.PhoneNumber;
            customer.FullName = updateRequest.FullName ?? customer.FullName;
            customer.Country = updateRequest.Country ?? customer.Country;
            customer.Email = updateRequest.Email ?? customer.Email;

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();

            DtoCustomerView customerView = new DtoCustomerView();
            customerView.Id = customer.Id;
            customerView.Address = customer.Address;
            customerView.PhoneNumber = customer.PhoneNumber;
            customerView.FullName = customer.FullName;
            customerView.Country = customer.Country;
            customerView.Email = customer.Email;

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

            DtoProductViewForOrder dtoProductViewForOrder = new DtoProductViewForOrder();
            dtoProductViewForOrder.Price = product.Price;
            dtoProductViewForOrder.Option = option1;
            dtoProductViewForOrder.Name = name;
            dtoProductViewForOrder.Id = product.Id;
            dtoProductViewForOrder.Category = product.Category;



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
          //  finalOrder.OrderDetails.Add(orderDetail);


            DtoOrderView dtoOrderView = new DtoOrderView();
            dtoOrderView.OrderAddress = finalOrder.OrderAddress;
            dtoOrderView.OrderDate = finalOrder.OrderDate;

            List<DtoOrderDetailView> dtoOrderDetailViews = new List<DtoOrderDetailView>();

            foreach(var op in finalOrder.OrderDetails)
            {
                DtoOrderDetailView dtoOrderDetailView = new DtoOrderDetailView();
                dtoOrderDetailView.Product = dtoProductViewForOrder;
                dtoOrderDetailView.Quantity = quantity;
                dtoOrderDetailView.Price = orderDetail.Price;
                dtoOrderDetailView.Id = orderDetail.Id;

                dtoOrderDetailViews.Add(dtoOrderDetailView);
            }

            dtoOrderView.Products = dtoOrderDetailViews;

            product.Stock -= quantity;

            _context.Products.Update(product);

            _context.Orders.Update(finalOrder);

            await _context.SaveChangesAsync();

          //  customer.Orders.Add(finalOrder);

            await _context.SaveChangesAsync();

            DtoCustomerView dtoCustomerView = new DtoCustomerView();
            dtoCustomerView.Id = customer.Id;
            dtoCustomerView.Address = customer.Address;
            dtoCustomerView.PhoneNumber = customer.PhoneNumber;
            dtoCustomerView.FullName = customer.FullName;
            dtoCustomerView.Country = customer.Country;
            dtoCustomerView.Email = customer.Email;

            List<DtoOrderView> views = new List<DtoOrderView>();

            foreach (var op in customer.Orders)
            {
                DtoOrderView dtoOrderView1 = new DtoOrderView();
                dtoOrderView1.Id = op.Id;
                dtoOrderView1.OrderAddress = op.OrderAddress;
                dtoOrderView1.OrderDate = op.OrderDate;
                dtoOrderView1.Status = op.Status;
                dtoOrderView1.Ammount = op.Ammount;

                List<DtoOrderDetailView> dtoODetailViews = new List<DtoOrderDetailView>();

                foreach (OrderDetail od in op.OrderDetails)
                {
                    DtoOrderDetailView dto = new DtoOrderDetailView();

                    dto.Id = od.Id;
                    dto.Price = od.Price;
                    dto.Quantity = od.Quantity;
                    DtoProductViewForOrder dtoProductView = new DtoProductViewForOrder();
                    Product p = await _context.Products.FindAsync(od.ProductId);
                    dtoProductView.Id = p.Id;
                    dtoProductView.Price = p.Price;
                    dtoProductView.Name = p.Name;
                    dtoProductView.Category = p.Category;
                    dtoProductView.Option = await _context.Options.FirstOrDefaultAsync(s=>s.Name == name);

                    dto.Product = dtoProductView;

                    dtoODetailViews.Add(dto);
                }

                dtoOrderView1.Products = dtoODetailViews;


                views.Add(dtoOrderView1);
            }


            dtoCustomerView.Orders = views;

            return dtoCustomerView;
        }

        public async Task<DtoCustomerView> DeleteOrder(int idCustomer, int idOrder)
        {
            var customer = await _context.Customers.FindAsync(idCustomer);

            var order = _context.Orders.FirstOrDefault(s=>s.CustomerId == idCustomer && s.Status == "open");

            if (order == null) return null;

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            DtoCustomerView dtoCustomerView = new DtoCustomerView();
            dtoCustomerView.Id = customer.Id;
            dtoCustomerView.Address = customer.Address;
            dtoCustomerView.PhoneNumber = customer.PhoneNumber;
            dtoCustomerView.FullName = customer.FullName;
            dtoCustomerView.Country = customer.Country;
            dtoCustomerView.Email = customer.Email;

            List<DtoOrderView> views = new List<DtoOrderView>();

            foreach (var op in customer.Orders)
            {
                DtoOrderView dtoOrderView1 = new DtoOrderView();
                dtoOrderView1.Id = op.Id;
                dtoOrderView1.OrderAddress = op.OrderAddress;
                dtoOrderView1.OrderDate = op.OrderDate;
                dtoOrderView1.Status = op.Status;
                dtoOrderView1.Ammount = op.Ammount;

                List<DtoOrderDetailView> dtoODetailViews = new List<DtoOrderDetailView>();

                foreach (OrderDetail od in op.OrderDetails)
                {
                    DtoOrderDetailView dto = new DtoOrderDetailView();

                    dto.Id = od.Id;
                    dto.Price = od.Price;
                    dto.Quantity = od.Quantity;
                    DtoProductViewForOrder dtoProductView = new DtoProductViewForOrder();
                    Product p = await _context.Products.FindAsync(od.ProductId);
                    dtoProductView.Id = p.Id;
                    dtoProductView.Price = p.Price;
                    dtoProductView.Name = p.Name;
                    dtoProductView.Category = p.Category;
                   // dtoProductView.Option = await _context.Options.FirstOrDefaultAsync(s => s.Name == name);


                    dto.Product = dtoProductView;

                    dtoODetailViews.Add(dto);
                }

                dtoOrderView1.Products = dtoODetailViews;


                views.Add(dtoOrderView1);
            }


            dtoCustomerView.Orders = views;

            return dtoCustomerView;
        }

        public async Task<DtoCustomerView> DeleteProductToOrder(int idCurtomer, string name, string option)
        {
            var customer = await _context.Customers.FindAsync(idCurtomer);

            var order = _context.Orders.Include(s=>s.OrderDetails).FirstOrDefault(s => s.CustomerId == idCurtomer && s.Status == "open");

            if (order == null) return null;

            var orderDetail = (OrderDetail)null;
            Product product = null;
            foreach (var item in order.OrderDetails)
            {
                if (_context.Products.FirstOrDefaultAsync(s=>s.Id == item.ProductId).Result.Name == name) { orderDetail = item; product = item.Product; }
            }

            if (orderDetail == null) return null;

            product.Stock += orderDetail.Quantity;

            _context.Products.Update(product);

            order.Ammount -= orderDetail.Price;

            _context.OrderDetails.Remove(orderDetail);

            _context.Orders.Update(order);

            await _context.SaveChangesAsync();

            DtoCustomerView dtoCustomerView = new DtoCustomerView();
            dtoCustomerView.Id = customer.Id;
            dtoCustomerView.Address = customer.Address;
            dtoCustomerView.PhoneNumber = customer.PhoneNumber;
            dtoCustomerView.FullName = customer.FullName;
            dtoCustomerView.Country = customer.Country;
            dtoCustomerView.Email = customer.Email;

            List<DtoOrderView> views = new List<DtoOrderView>();

            foreach (var op in customer.Orders)
            {
                DtoOrderView dtoOrderView1 = new DtoOrderView();
                dtoOrderView1.Id = op.Id;
                dtoOrderView1.OrderAddress = op.OrderAddress;
                dtoOrderView1.OrderDate = op.OrderDate;
                dtoOrderView1.Status = op.Status;
                dtoOrderView1.Ammount = op.Ammount;

                List<DtoOrderDetailView> dtoODetailViews = new List<DtoOrderDetailView>();

                foreach (OrderDetail od in op.OrderDetails)
                {
                    DtoOrderDetailView dto = new DtoOrderDetailView();

                    dto.Id = od.Id;
                    dto.Price = od.Price;
                    dto.Quantity = od.Quantity;
                    DtoProductViewForOrder dtoProductView = new DtoProductViewForOrder();
                    Product p = await _context.Products.FindAsync(od.ProductId);
                    dtoProductView.Id = p.Id;
                    dtoProductView.Price = p.Price;
                    dtoProductView.Name = p.Name;
                    dtoProductView.Category = p.Category;
                    dtoProductView.Option = await _context.Options.FirstOrDefaultAsync(s => s.Name == name);


                    dto.Product = dtoProductView;

                    dtoODetailViews.Add(dto);
                }

                dtoOrderView1.Products = dtoODetailViews;


                views.Add(dtoOrderView1);
            }


            dtoCustomerView.Orders = views;

            return dtoCustomerView;
        }

    }
}

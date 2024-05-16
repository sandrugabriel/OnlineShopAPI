using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Dto;
using OnlineShop.Orders.Repository.interfaces;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;

namespace OnlineShop.Orders.Repository
{
    public class RepositoryOrder : IRepositoryOrder
    {
        AppDbContext _context;
        IMapper _mapper;

        public RepositoryOrder(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
       
        }

        public async Task<List<DtoOrderView>> GetAllAsync()
        {
            List<Order> orders = await _context.Orders.Include(s => s.OrderDetails).ToListAsync();

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


                    dto.Product = dtoProductView;

                    dtoODetailViews.Add(dto);
                }

                dtoOrderView.Products = dtoODetailViews;

                orderViews.Add(dtoOrderView);
            }

            return orderViews;
        }

        public async Task<Order> GetById(int id)
        {
            List<Order> orders = await _context.Orders.Include(s => s.OrderDetails).ToListAsync();

            foreach (Order order in orders)
            {
                if(order.Id == id) return order;
            }

            return null;
        }

        public async Task<DtoOrderView> GetByIdAsync(int id)
        {

            List<Order> orders = await _context.Orders.Include(s => s.OrderDetails).ToListAsync();

            var order = (Order)null;
            foreach (Order order1 in orders)
            {
                if (order1.Id == id) order = order1;
            }

            if (order == null) return null;

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


                dto.Product = dtoProductView;

                dtoODetailViews.Add(dto);
            }

            dtoOrderView.Products = dtoODetailViews;


            return dtoOrderView;
        }
    }
}

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
                DtoOrderView dtoOrderView = _mapper.Map<DtoOrderView>(order);

                List<DtoOrderDetailView> dtoODetailViews = new List<DtoOrderDetailView>();

                foreach (OrderDetail orderDetail in order.OrderDetails)
                {
                    DtoOrderDetailView dto = _mapper.Map<DtoOrderDetailView>(orderDetail);

                    Product product = await _context.Products.FindAsync(orderDetail.ProductId);
                    DtoProductViewForOrder dtoProductView = _mapper.Map<DtoProductViewForOrder>(product);


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

            DtoOrderView dtoOrderView = _mapper.Map<DtoOrderView>(order);

            List<DtoOrderDetailView> dtoODetailViews = new List<DtoOrderDetailView>();

            foreach (OrderDetail orderDetail in order.OrderDetails)
            {
                DtoOrderDetailView dto = _mapper.Map<DtoOrderDetailView>(orderDetail);

                Product product = await _context.Products.FindAsync(orderDetail.ProductId);
                DtoProductViewForOrder dtoProductView = _mapper.Map<DtoProductViewForOrder>(product);


                dto.Product = dtoProductView;

                dtoODetailViews.Add(dto);
            }

            dtoOrderView.Products = dtoODetailViews;


            return dtoOrderView;
        }

        public async void SaveOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}

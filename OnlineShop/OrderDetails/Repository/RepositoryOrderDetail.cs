using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;
using OnlineShop.OrderDetails.Repository.interfaces;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;

namespace OnlineShop.OrderDetailDetails.Repository
{
    public class RepositoryOrderDetail : IRepositoryOrderDetail
    {
        AppDbContext _context;
        IMapper _mapper;

        public RepositoryOrderDetail(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<DtoOrderDetailView>> GetAllAsync()
        {
            List<OrderDetail> orderDetails = await _context.OrderDetails.ToListAsync();

            List<DtoOrderDetailView> orderDetailsViews = new List<DtoOrderDetailView>();

            foreach (OrderDetail orderDetail in orderDetails)
            {
                DtoOrderDetailView dtoOrderView = new DtoOrderDetailView();
                dtoOrderView.Id = orderDetail.Id;
                dtoOrderView.Price = orderDetail.Price;
                dtoOrderView.Quantity = orderDetail.Quantity;
                DtoProductViewForOrder dtoProductView = new DtoProductViewForOrder();
                Product product = await _context.Products.FindAsync(orderDetail.ProductId);
                dtoProductView.Id = product.Id;
                dtoProductView.Price = product.Price;
                dtoProductView.Name = product.Name;
                dtoProductView.Category = product.Category;

                dtoOrderView.Product = dtoProductView;

                orderDetailsViews.Add(dtoOrderView);
            }

            return orderDetailsViews;

        }

        public async Task<DtoOrderDetailView> GetByIdAsync(int id)
        {

            List<OrderDetail> orderDetails = await _context.OrderDetails.ToListAsync();

            var orderDetail = (OrderDetail)null;
            foreach (OrderDetail orderDetail1 in orderDetails)
            {
                if (orderDetail1.Id == id) orderDetail = orderDetail1;
            }

            if (orderDetail == null) return null;


            DtoOrderDetailView dtoOrderView = new DtoOrderDetailView();
            dtoOrderView.Price = orderDetail.Price;
            dtoOrderView.Quantity = orderDetail.Quantity;
            DtoProductViewForOrder dtoProductView = new DtoProductViewForOrder();
            Product product = await _context.Products.FindAsync(orderDetail.ProductId);
            dtoProductView.Price = product.Price;
            dtoProductView.Name = product.Name;
            dtoProductView.Category = product.Category;


            dtoOrderView.Product = dtoProductView;


            return dtoOrderView;
        }

        public async Task<OrderDetail> GetById(int id)
        {

            List<OrderDetail> orderDetails = await _context.OrderDetails.ToListAsync();

            foreach (OrderDetail orderDetail1 in orderDetails)
            {
                if (orderDetail1.Id == id) return orderDetail1;
            }

        
            return null;
        }
    }
}

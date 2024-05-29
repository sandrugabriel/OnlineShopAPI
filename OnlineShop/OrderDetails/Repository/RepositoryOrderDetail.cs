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

            return _mapper.Map<List<DtoOrderDetailView>>(orderDetails);

        }

        public async Task<DtoOrderDetailView> GetByIdAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(s=>s.Id == id);
            return _mapper.Map<DtoOrderDetailView>(orderDetail);
        }

        public async Task<OrderDetail> GetById(int id)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(s => s.Id == id);

        }

        public async void SaveOrderDetails(List<OrderDetail> orderDetail)
        {
            _context.OrderDetails.AddRange(orderDetail);
             await _context.SaveChangesAsync();
        }
    }
}

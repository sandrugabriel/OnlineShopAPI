using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Dto;
using OnlineShop.Orders.Repository.interfaces;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;
using System.Data;

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
            var orders = await _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).ToListAsync();

            return _mapper.Map<List<DtoOrderView>>(orders);
        }

        public async Task<Order> GetById(int id)
        {
            // List<Order> orders = await _context.Orders.Include(s => s.OrderDetails).ToListAsync();

            return await _context.Orders.Include(s => s.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<DtoOrderView> GetByIdAsync(int id)
        {
            var order = await _context.Orders.Include(s => s.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);
            return _mapper.Map<DtoOrderView>(order);

        }

        public async Task SaveOrder(Order order)
        {
              _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}

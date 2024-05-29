using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.ProductOptions.Model;
using OnlineShop.ProductOptions.Repository.interfaces;
namespace OnlineShop.ProductOptions.Repository
{
    public class RepositoryProductOption : IRepositoryProductOption
    {

        AppDbContext _context;
        IMapper _mapper;

        public RepositoryProductOption(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<ProductOption>> GetAllAsync()
        {
            return await _context.ProductOptions.ToListAsync();

        }

        public async Task<ProductOption> GetByIdAsync(int id)
        {
            return _context.ProductOptions.FirstOrDefault(s=>s.Id == id);
        }


    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;
using OnlineShop.Options.Repository.interfaces;
using OnlineShop.ProductOptions.Model;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;

namespace OnlineShop.Options.Repository
{
    public class RepositoryOption : IRepositoryOption
    {
        AppDbContext _context;
        IMapper _mapper;

        public RepositoryOption(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Option> CreateOption(CreateRequestOption createRequest)
        {

            var option = _mapper.Map<Option>(createRequest);
            option.ProductOption = new ProductOption();
            _context.Options.Add(option);

            await _context.SaveChangesAsync();

            return option;
        }

        public async Task<Option> DeleteOption(int id)
        {
            var option = await _context.Options.FindAsync(id);

            if (option == null) return null;

            _context.Options.Remove(option);

            await _context.SaveChangesAsync();

            return option;
        }

        public async Task<List<Option>> GetAllAsync()
        {
            return await _context.Options.Include(s => s.ProductOption).ToListAsync();

        }

        public async Task<Option> GetByIdAsync(int id)
        {
            return await _context.Options.Include(s => s.ProductOption).FirstOrDefaultAsync(s=>s.Id == id);
        }

        public async Task<Option> GetByNameAsync(string name)
        {
            return await _context.Options.Include(s => s.ProductOption).FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<Option> UpdateOption(int id, UpdateRequestOption updateRequest)
        {
            var option = await _context.Options.FindAsync(id);

            option.Name = updateRequest.Name ?? option.Name;
            option.Price = updateRequest.Price ?? option.Price;

            _context.Options.Update(option);

            await _context.SaveChangesAsync();

            return option;
        }
    }
}

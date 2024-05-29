using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Options.Models;
using OnlineShop.ProductOptions.Model;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;
using OnlineShop.Products.Repository.interfaces;

namespace OnlineShop.Products.Repository
{
    public class RepositoryProduct : IRepositoryProduct
    {

        private AppDbContext _context;
        private IMapper _mapper;
        public RepositoryProduct(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DtoProductView>> GetAllAsync()
        {
            List<Product> products = await _context.Products.ToListAsync();

            return _mapper.Map<List<DtoProductView>>(products);
        }
        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(s=>s.Id == id);
        }
        public async Task<DtoProductView> GetByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(s=>s.Id == id);


            return _mapper.Map<DtoProductView>(product);
        }

        public async Task<DtoProductView> GetByNameAsync(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(s => s.Name == name);


            return _mapper.Map<DtoProductView>(product);
        }

        public async Task<Product> GetByName(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(s => s.Name == name);
        }
        public async Task<DtoProductView> Create(CreateRequestProduct request)
        {
            var product = _mapper.Map<Product>(request);

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            product.Create_date = DateTime.Now;
            return _mapper.Map<DtoProductView>(product);
        }
        public async Task<DtoProductView> UpdateAsync(int id, UpdateRequestProduct request)
        {

            var product = await _context.Products.FindAsync(id);

            product.Name = request.Name ?? product.Name;
            product.Price = request.Price ?? product.Price;
            product.Category = request.Category ?? product.Category;
            product.Stock = request.Stock ?? product.Stock;

            _context.Update(product);
            await _context.SaveChangesAsync();

           return _mapper.Map<DtoProductView>(product);

        }
        public async Task<DtoProductView> DeleteById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();


            return _mapper.Map<DtoProductView>(product);
        }
        public async Task<DtoProductView> AddOption(int id, string name)
        {
            var product = await _context.Products.Include(s => s.OrderDetails).Include(s=>s.ProductOptions).FirstOrDefaultAsync(s=>s.Id==id);
            List<ProductOption> productOptions = product.ProductOptions;

            List<Option> options = await _context.Options.Include(s=>s.ProductOption).ToListAsync();

            foreach(var option in options)
            {
                if(option.ProductOption != null)
                if (option.Name == name && option.ProductOption.IdProduct == id) return null; 
            }

            Option productoption = options.FirstOrDefault(s => s.Name == name);

            if (productoption == null) return null;

            ProductOption finalOption = new ProductOption();
            finalOption.Product = product;
            finalOption.Option = productoption;
            finalOption.IdOption = productoption.Id;
            finalOption.IdProduct = product.Id;

             _context.ProductOptions.Add(finalOption);

            await _context.SaveChangesAsync();


           return _mapper.Map<DtoProductView>(product);
        }

        public async Task<DtoProductView> DeleteOption(int id, string name)
        {
            var product = await _context.Products.Include(s => s.OrderDetails).Include(s => s.ProductOptions).FirstOrDefaultAsync(s => s.Id == id);
            List<ProductOption> productOptions = product.ProductOptions;
            if (productOptions == null) return null;
            var options = await _context.Options.Include(s => s.ProductOption).ToListAsync();

            var option = options.FirstOrDefault(s => s.ProductOption != null & s.Name == name && s.ProductOption.IdProduct == id);

            if (option == null)
            {
                return null;
            }

            
            _context.ProductOptions.Remove(option.ProductOption);

            await _context.SaveChangesAsync();

            return _mapper.Map<DtoProductView>(product);
        }
    }
}

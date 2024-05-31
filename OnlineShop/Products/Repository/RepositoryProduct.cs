using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;
using OnlineShop.ProductOptions.Model;
using OnlineShop.Products.Dto;
using OnlineShop.Products.Models;
using OnlineShop.Products.Repository.interfaces;
using System.Data.Entity.ModelConfiguration.Configuration;

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

            var responce = _mapper.Map<List<DtoProductView>>(products);
            int index = 0;
            foreach (var product in products)
            {
                responce[index].Options = _mapper.Map<List<OptionResponse>>(product.ProductOptions);
                index++;
            }
           
            return responce;
        }
        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(s=>s.Id == id);
        }
        public async Task<DtoProductView> GetByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(s=>s.Id == id);

            var responce = _mapper.Map<DtoProductView>(product);
            responce.Options = _mapper.Map<List<OptionResponse>>(product.ProductOptions);
            return responce;
        }

        public async Task<DtoProductView> GetByNameAsync(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(s => s.Name == name);

            var responce = _mapper.Map<DtoProductView>(product);
            responce.Options = _mapper.Map<List<OptionResponse>>(product.ProductOptions);
            return responce;
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
            var responce = _mapper.Map<DtoProductView>(product);
            responce.Options = _mapper.Map<List<OptionResponse>>(product.ProductOptions);
            return responce;
        }
        public async Task<DtoProductView> UpdateAsync(int id, UpdateRequestProduct request)
        {

            var product = await _context.Products.FindAsync(id);

            product.Name = request.Name ?? product.Name;
            product.Price = request.Price ?? product.Price;
            product.Category = request.Category ?? product.Category;
            product.Stock = request.Stock ?? product.Stock;

            _context.Products.Update(product);

            await _context.SaveChangesAsync();
            var responce = _mapper.Map<DtoProductView>(product);
            responce.Options = _mapper.Map<List<OptionResponse>>(product.ProductOptions);
            return responce;
        }
        public async Task<DtoProductView> DeleteById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();


            var responce = _mapper.Map<DtoProductView>(product);
            responce.Options = _mapper.Map<List<OptionResponse>>(product.ProductOptions);
            return responce;
        }
        public async Task<DtoProductView> AddOption(int id, Option option)
        {
            Product product = await _context.Products.Include(s => s.OrderDetails).Include(s => s.ProductOptions).ThenInclude(s => s.Option).FirstOrDefaultAsync(s => s.Id == id);
            ProductOption productOption = new ProductOption();
            productOption.Option = option;
            productOption.Product = product;
            productOption.IdProduct = id;
            productOption.IdOption = option.Id;
        
            product.ProductOptions.Add(productOption);
            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            var responce = _mapper.Map<DtoProductView>(product);
            responce.Options = _mapper.Map<List<OptionResponse>>(product.ProductOptions);

           return responce;
        }

        public async Task<DtoProductView> DeleteOption(int id, string name)
        {
            var product = await _context.Products.Include(s => s.OrderDetails).Include(s => s.ProductOptions).FirstOrDefaultAsync(s => s.Id == id);

            product.ProductOptions.Remove(product.ProductOptions.FirstOrDefault(s => s.Option.Name == name));

            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            var responce = _mapper.Map<DtoProductView>(product);
            responce.Options = _mapper.Map<List<OptionResponse>>(product.ProductOptions);
            return responce;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
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

            List<DtoProductView> productViews = new List<DtoProductView>();

            foreach (var product in products)
            {
                DtoProductView dtoProductView = new DtoProductView();
                dtoProductView.Id = product.Id;
                dtoProductView.Name = product.Name;
                dtoProductView.Price = product.Price;
                dtoProductView.Create_date = product.Create_date;
                dtoProductView.Category = product.Category;
                dtoProductView.Stock = product.Stock;

                productViews.Add(dtoProductView);
            }

            return productViews;
        }
        public async Task<Product> GetById(int id)
        {
            List<Product> products = await _context.Products.ToListAsync();

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == id)
                {
                    return products[i];
                }
            }
            return null;
        }
        public async Task<DtoProductView> GetByIdAsync(int id)
        {
            List<Product> products = await _context.Products.ToListAsync();

            var product = (Product)null;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == id)
                {
                    product = products[i];
                    break;
                }
            }

            if (product == null) return null;

            DtoProductView dtoProductView = new DtoProductView();
            dtoProductView.Id = product.Id;
            dtoProductView.Name = product.Name;
            dtoProductView.Price = product.Price;
            dtoProductView.Create_date = product.Create_date;
            dtoProductView.Category = product.Category;
            dtoProductView.Stock = product.Stock;


            return dtoProductView;
        }

        public async Task<DtoProductView> GetByNameAsync(string name)
        {
            List<Product> products = await _context.Products.ToListAsync();


            var product = (Product)null;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name.Equals(name))
                {
                    product = products[i];
                    break;
                }
            }
            if (product == null) return null;

            DtoProductView dtoProductView = new DtoProductView();
            dtoProductView.Id = product.Id;
            dtoProductView.Name = product.Name;
            dtoProductView.Price = product.Price;
            dtoProductView.Create_date = product.Create_date;
            dtoProductView.Category = product.Category;
            dtoProductView.Stock = product.Stock;

            return dtoProductView;
        }

        public async Task<Product> GetByName(string name)
        {
            List<Product> products = await _context.Products.ToListAsync();

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name.Equals(name))
                {
                    return products[i];
                    break;
                }
            }

            return null;
        }


        public async Task<DtoProductView> Create(CreateRequestProduct request)
        {
            var product = _mapper.Map<Product>(request);

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            product.Create_date = DateTime.Now;
            DtoProductView dtoProductView = new DtoProductView();
            dtoProductView.Id = product.Id;
            dtoProductView.Name = product.Name;
            dtoProductView.Price = product.Price;
            dtoProductView.Create_date = DateTime.Now;
            dtoProductView.Category = product.Category;
            dtoProductView.Stock = product.Stock;

            return dtoProductView;
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

            DtoProductView dtoProductView = new DtoProductView();
            dtoProductView.Id = product.Id;
            dtoProductView.Name = product.Name;
            dtoProductView.Price = product.Price;
            dtoProductView.Create_date = product.Create_date;
            dtoProductView.Category = product.Category;
            dtoProductView.Stock = product.Stock;

            return dtoProductView;

        }

        public async Task<DtoProductView> DeleteById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();


            DtoProductView dtoProductView = new DtoProductView();
            dtoProductView.Id = product.Id;
            dtoProductView.Name = product.Name;
            dtoProductView.Price = product.Price;
            dtoProductView.Create_date = product.Create_date;
            dtoProductView.Category = product.Category;
            dtoProductView.Stock = product.Stock;

            return dtoProductView;
        }


    }
}

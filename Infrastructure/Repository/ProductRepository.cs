using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProdSalesContext _context;
        public ProductRepository(ProdSalesContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product product, int userId, bool isAdmin)
        {
            product.UserId = userId;
            product.IsAdmin = isAdmin;

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id, int userId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int id, int userId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        

        public async Task UpdateProduct(Product product, int userId)
        {
            product.UserId = userId;

            _context.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetProductsOfUser(int userId)
        {
            return _context.Products.Where(p => p.UserId == userId).ToList();
        }
    }
}

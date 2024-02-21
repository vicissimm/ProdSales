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

        public async Task CreateProduct(Product product, string accessToken)
        {
            var id = GetUserIdFromToken(accessToken);
            product.UserId = id;

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id, string accessToken)
        {
            var userId = GetUserIdFromToken(accessToken);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int id, string accessToken)
        {
            var userId = GetUserIdFromToken(accessToken);
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        

        public async Task UpdateProduct(Product product, string accessToken)
        {
            var id = GetUserIdFromToken(accessToken);

            product.UserId = id;

            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public int GetUserIdFromToken(string token)
        {
            var configurationManager = new ConfigurationManager();
            var auth = new Authorization(configurationManager);
            var userObj = auth.DecodeAccessToken(token);

            return userObj.Id;
        }

        public async Task<List<Product>> GetProductsOfUser(string accessToken)
        {
            var userId = GetUserIdFromToken(accessToken);
            return _context.Products.Where(p => p.UserId == userId).ToList();
        }
    }
}

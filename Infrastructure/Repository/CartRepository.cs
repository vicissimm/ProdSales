using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ProdSalesContext _context;

        public CartRepository(ProdSalesContext context)
        {
            _context = context;
        }
        public async Task AddProductToCart(Cart cart, string accessToken)
        {
            var userId = GetUserIdFromToken(accessToken);
            cart.UserId = userId;

            await _context.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductFromCart(string accessToken, int productId)
        {
            var userId = GetUserIdFromToken(accessToken);
            var product = await _context.Carts.FirstOrDefaultAsync(p => p.ProductId == productId);

            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsInCart(string accessToken)
        {
            var userId = GetUserIdFromToken(accessToken);
            var productsInCart = await _context.Carts
                .Where(c => c.UserId == userId)
                .Select(c => c.Product)
                .ToListAsync();

            return productsInCart;
        }

        public int GetUserIdFromToken(string token)
        {
            var configurationManager = new ConfigurationManager();
            var auth = new Authorization(configurationManager);
            var userObj = auth.DecodeAccessToken(token);

            return userObj.Id;
        }
    }
}

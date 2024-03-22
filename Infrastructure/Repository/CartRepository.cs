using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ProdSalesContext _context;

        public CartRepository(ProdSalesContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

        }
        public async Task AddProductToCart(Cart cart, int userId)
        {
            cart.UserId = userId;

            await _context.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductFromCart(int userId, int productId)
        {
            
            var product = await _context.Carts.FirstOrDefaultAsync(p => p.Id == productId);

            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsInCart(int userId)
        {
            var productsInCart = await _context.Carts
                .Where(c => c.UserId == userId)
                .Select(c => c.Product)
                .ToListAsync();

            return productsInCart;
        }
    }
}

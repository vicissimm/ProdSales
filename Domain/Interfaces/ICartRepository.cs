
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICartRepository
    {
        public Task AddProductToCart(Cart cart , int userId); 
        public Task DeleteProductFromCart(int userId, int productId);
        public Task<List<Product>> GetProductsInCart(int userId);
    }
}

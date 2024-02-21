
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICartRepository
    {
        public Task AddProductToCart(Cart cart ,string accessToken); 
        public Task DeleteProductFromCart(string accessToken, int productId);
        public Task<List<Product>> GetProductsInCart(string accessToken);
    }
}

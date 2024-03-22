
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProductsOfUser(int userId);
        public Task<Product> GetProductById(int id, int userId);
        public Task CreateProduct(Product product, int userId, bool isAdmin);
        public Task UpdateProduct(Product product, int userId);
        public Task DeleteProduct(int id, int userId);
    }
}

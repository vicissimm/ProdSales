
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProductsOfUser(string accessToken);
        public Task<Product> GetProductById(int id, string accessToken);
        public Task CreateProduct(Product product, string accessToken);
        public Task UpdateProduct(Product product, string accessToken);
        public Task DeleteProduct(int id, string accessToken);
    }
}

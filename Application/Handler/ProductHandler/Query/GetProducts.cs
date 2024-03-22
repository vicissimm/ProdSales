using Domain.Entities;
using Domain.Interfaces;
using Convey.CQRS.Queries;

namespace Application.Handler.ProductHandler.Query
{
    public class GetProductsQuery : IQuery<List<Product>>
    {
        public int UserId { get; set; }
    }
    public class GetProducts : IQueryHandler<GetProductsQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;
        
        public GetProducts(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken = default)
        {
            return await _productRepository.GetProductsOfUser(query.UserId);
        }
    }
}

using Convey.CQRS.Queries;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;
using Application.Route;

namespace Application.Handler.ProductHandler.Query
{
    public class GetProductQuery : IQuery<Product>
    {
        public int Id { get; set; }
        public string AccessToken { get; set; } = string.Empty;
    }
    public class GetProduct : IQueryHandler<GetProductQuery, Product>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProduct(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<Product> HandleAsync(GetProductQuery query, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetProductById(query.Id, query.AccessToken);
            var productDto = _mapper.Map<Product>(product);
            return productDto;
        }
    }
}

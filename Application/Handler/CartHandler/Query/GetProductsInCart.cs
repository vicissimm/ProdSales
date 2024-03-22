using Convey.CQRS.Queries;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.CartHandler.Query
{
    public class GetProductsInCartQuery : IQuery<List<Product>>
    {
        public int UserId { get; set; }
    }
    public class GetProductsInCart : IQueryHandler<GetProductsInCartQuery, List<Product>>
    {
        private readonly ICartRepository _cartRepository;
        public GetProductsInCart(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<List<Product>> HandleAsync(GetProductsInCartQuery query, CancellationToken cancellationToken = default)
        {
            return await _cartRepository.GetProductsInCart(query.UserId);
        }
    }
}

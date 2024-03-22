using Convey.CQRS.Commands;
using Application.Dto;
using Domain.Interfaces;
using Domain.Entities;
using AutoMapper;

namespace Application.Handler.ProductHandler.Command
{
    public class UpdateProductCommand : ICommand
    {
        public ProductDto Product { get; set; } = default!;
        public int UserId { get; set; }

    }
    public class UpdateProduct : ICommandHandler<UpdateProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public UpdateProduct(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken = default)
        {
            var product = _mapper.Map<Product>(command.Product);

            await _productRepository.UpdateProduct(product, command.UserId);
        }
    }
}

using Convey.CQRS.Commands;
using Application.Dto;
using Domain.Interfaces;
using Domain.Entities;
using AutoMapper;

namespace Application.Handler.ProductHandler.Command
{

    public class AddProductCommand : ICommand
    {
        public ProductDto Product { get; set; } = default!;
        public string AccessToken { get; set; } = string.Empty;
    }
    public class AddProduct : ICommandHandler<AddProductCommand>
    {
        private readonly IProductRepository _productRepostory;
        private readonly IMapper _mapper;
        
        public AddProduct(IProductRepository productRepository, IMapper mapper)
        {
            _productRepostory = productRepository;
            _mapper = mapper;
        }

        public async Task HandleAsync(AddProductCommand command, CancellationToken cancellationToken = default)
        {
            var product = _mapper.Map<Product>(command.Product);
            await _productRepostory.CreateProduct(product, command.AccessToken);
        }
    }
   
}

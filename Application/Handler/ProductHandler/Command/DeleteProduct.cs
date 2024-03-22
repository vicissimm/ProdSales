using Convey.CQRS.Commands;
using Application.Route;
using Application.Dto;
using Domain.Interfaces;
using Domain.Entities;
using AutoMapper;

namespace Application.Handler.ProductHandler.Command
{
    public class DeleteProductCommand : ICommand
    {
        public DeleteProductRoute Product { get; set; } = default!;
        public int UserId { get; set; }

    }
    public class DeleteProduct : ICommandHandler<DeleteProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public DeleteProduct(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken = default)
        {
            await _productRepository.DeleteProduct(command.Product.Id, command.UserId);
        }
    }

}

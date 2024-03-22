using Application.Dto;
using AutoMapper;
using Convey.CQRS.Commands;
using Domain.Entities;
using Domain.Interfaces;
using System.Runtime.CompilerServices;


namespace Application.Handler.CartHandler.Command
{
    public class AddProductToCartCommand : ICommand
    {
        public CartDto Cart { get; set; } = default!;
        public int UserId { get; set; }
    }
    public class AddProductToCart : ICommandHandler<AddProductToCartCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public AddProductToCart(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task HandleAsync(AddProductToCartCommand command, CancellationToken cancellationToken = default)
        {
            var product = _mapper.Map<Cart>(command.Cart);
            await _cartRepository.AddProductToCart(product, command.UserId);
        }
    }
}

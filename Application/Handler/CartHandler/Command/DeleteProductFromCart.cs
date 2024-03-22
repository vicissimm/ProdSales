using Application.Route;
using Convey.CQRS.Commands;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.CartHandler.Command
{
    public class DeleteProductFromCartCommand : ICommand
    {
        public DeleteProductFromCartRoute Product { get; set; }
        public int UserId { get; set; }
    }
    public class DeleteProductFromCart : ICommandHandler<DeleteProductFromCartCommand>
    {
        private readonly ICartRepository _cartRepository;
        public DeleteProductFromCart(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task HandleAsync(DeleteProductFromCartCommand command, CancellationToken cancellationToken = default)
        {
            await _cartRepository.DeleteProductFromCart(command.UserId, command.Product.Id);
        }
    }
}

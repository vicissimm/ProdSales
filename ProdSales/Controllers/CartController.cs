using Application.Dto;
using Application.Handler.CartHandler.Command;
using Application.Handler.CartHandler.Query;
using Application.Route;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Replication.PgOutput.Messages;

namespace ProdSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public CartController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]

        public async Task<IActionResult> GetProductsInCart([FromHeader] string accessToken)
        {
            var query = new GetProductsInCartQuery();
            query.AccessToken = accessToken;

            var products = await _queryDispatcher.QueryAsync(query);
            return Ok(products);
        }

        [HttpPost]

        public async Task<IActionResult> AddProductInCart([FromBody] CartDto cart ,[FromHeader] string accessToken)
        {
            var command = new AddProductToCartCommand();
            command.Cart = cart;
            command.AccessToken = accessToken;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProductInCart(DeleteProductFromCartRoute route, [FromHeader] string accessToken)
        {
            var command = new DeleteProductFromCartCommand();
            command.AccessToken = accessToken;
            command.Product = route;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }
    }
}

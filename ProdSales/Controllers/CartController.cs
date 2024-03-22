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
            HttpContext context = HttpContext;

            if (!context.Items.ContainsKey("UserId"))
            {
                return Unauthorized();
            }

            var userId = context.Items["UserId"] as int?;

            if (!userId.HasValue)
            {
                // UserId is null, handle the scenario gracefully (e.g., log and return an error response)
                Console.WriteLine("UserId is null in AddProductInCart method.");
                return BadRequest("UserId is null.");
            }

            var query = new GetProductsInCartQuery();

            query.UserId = userId.Value;

            var products = await _queryDispatcher.QueryAsync(query);
            return Ok(products);
        }

        [HttpPost]

        public async Task<IActionResult> AddProductInCart([FromBody] CartDto cart, [FromHeader] string accessToken)
        {
            HttpContext context = HttpContext;

            if (!context.Items.ContainsKey("UserId"))
            {
                return Unauthorized();
            }

            var userId = context.Items["UserId"] as int?;

            if (!userId.HasValue)
            {
                // UserId is null, handle the scenario gracefully (e.g., log and return an error response)
                Console.WriteLine("UserId is null in AddProductInCart method.");
                return BadRequest("UserId is null.");
            }

            var command = new AddProductToCartCommand();
            command.Cart = cart;

            command.UserId = userId.Value;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProductInCart(DeleteProductFromCartRoute route, [FromHeader] string accessToken)
        {
            HttpContext context = HttpContext;

            if (!context.Items.ContainsKey("UserId"))
            {
                return Unauthorized();
            }

            var userId = context.Items["UserId"] as int?;

            if (!userId.HasValue)
            {
                // UserId is null, handle the scenario gracefully (e.g., log and return an error response)
                Console.WriteLine("UserId is null in AddProductInCart method.");
                return BadRequest("UserId is null.");
            }
            var command = new DeleteProductFromCartCommand();
            command.Product = route;

            command.UserId = userId.Value;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }
    }
}

using Convey.CQRS.Commands;
using Microsoft.AspNetCore.Mvc;
using Convey.CQRS.Queries;
using Application.Route;
using Application.Handler.ProductHandler.Query;
using Application.Dto;
using Application.Handler.ProductHandler.Command;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ProdSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ProductController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }


        [HttpPost]

        public async Task<IActionResult> CreateProduct([FromBody] ProductDto createProduct, [FromHeader] string accessToken)
        {
            HttpContext context = HttpContext;

            if (!context.Items.ContainsKey("UserId"))
            {
                return Unauthorized();
            }

            var userId = context.Items["UserId"] as int?;
            var isAdmin = context.Items["IsAdmin"] as bool?;

            if (!userId.HasValue)
            {
                Console.WriteLine("UserId is null in AddProductInCart method.");
                return BadRequest("UserId is null.");
            }
            var command = new AddProductCommand();
            command.Product = createProduct;
            command.UserId = userId.Value;
            command.IsAdmin = isAdmin.Value;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProduct(UpdateProductRoute route, [FromBody] ProductDto updateProduct,[FromHeader] string accessToken)
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
            updateProduct.SetId(route.Id);

            var command = new UpdateProductCommand();
            command.Product = updateProduct;
            command.UserId = userId.Value;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(DeleteProductRoute route, [FromHeader] string accessToken)
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
            var command = new DeleteProductCommand();
            command.Product = route;
            command.UserId = userId.Value;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProduct(GetProductRoute route,[FromHeader] string accessToken)
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
            var query = new GetProductQuery();
            query.Id = route.Id;
            query.UserId = userId.Value;

            var product = await _queryDispatcher.QueryAsync(query);
            return Ok(product);
        }
    }
}

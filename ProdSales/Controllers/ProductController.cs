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
            var command = new AddProductCommand();
            command.Product = createProduct;
            command.AccessToken = accessToken;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProduct(UpdateProductRoute route, [FromBody] ProductDto updateProduct,[FromHeader] string accessToken)
        {
            updateProduct.SetId(route.Id);

            var command = new UpdateProductCommand();
            command.Product = updateProduct;
            command.AccessToken = accessToken;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(DeleteProductRoute route, [FromHeader] string accessToken)
        {
            var command = new DeleteProductCommand();
            command.Product = route;
            command.AccessToken = accessToken;

            await _commandDispatcher.SendAsync(command);
            return Ok();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProduct(GetProductRoute route,[FromHeader] string accessToken)
        {
            var query = new GetProductQuery();
            query.Id = route.Id;
            query.AccessToken = accessToken;

            var product = await _queryDispatcher.QueryAsync(query);
            return Ok(product);
        }
    }
}

using Convey.CQRS.Commands;
using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Convey.CQRS.Queries;

namespace ProdSales.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDto createUser)
        {
            await _commandDispatcher.SendAsync(createUser);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto user)
        {
            var login = await _queryDispatcher.QueryAsync(user);

            return Ok(login);
        }
    }
}

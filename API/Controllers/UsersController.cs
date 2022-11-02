using API.Core.DTOs;
using API.Core.Errors;
using Application.DTOs;
using Application.UseCases.Queries;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UseCaseHandler _useCaseHandler;

        public UsersController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET api/<UsersController>/peraperic
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalExceptionError), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(GlobalExceptionError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("text/plain")]
        [Produces("application/json")]
        public IActionResult Get(string username, [FromServices] IGetUserQuery query)
        {
            return Ok(_useCaseHandler.HandleQuery(query, username));
        }
    }
}

using API.Core.Errors;
using Application.DTOs;
using Application.UseCases.Commands;
using Implementation.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        // POST api/<RegisterController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<ValidationError>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] UseCaseHandler handler, [FromServices] IRegisterUserCommand _command)
        {
            handler.HandleCommand(_command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}

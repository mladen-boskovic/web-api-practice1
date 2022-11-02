using API.Core;
using API.Core.DTOs;
using API.Core.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // POST api/<TokenController>
        [HttpPost]
        [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalExceptionError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Post([FromBody] TokenRequestDto dto, [FromServices] JwtManager jwtManager)
        {
            return Ok(jwtManager.MakeToken(dto));
        }
    }
}

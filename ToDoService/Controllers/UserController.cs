using Application.User.Command.AuthenticateUser;
using Application.User.Command.RegisterUser;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ToDoService.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/{v:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Method to to validate login
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserAuthResult), (int)HttpStatusCode.OK)]
        [Route("Login")]
        public async Task<ActionResult> Authenticate([FromBody]AuthenticateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Method to register new user based on user type
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Domain.Models.User), (int)HttpStatusCode.OK)]
        [Route("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromBody]RegisterUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
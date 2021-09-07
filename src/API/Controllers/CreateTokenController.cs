using System.Threading;
using System.Threading.Tasks;
using API.Controllers.Base;
using Application.Flows.Authentication.Commands;
using Application.Models.Authentication;
using Application.Request;
using Infrastructure.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    /// <summary>
    /// Token query API
    /// </summary>
    public class CreateTokenController : ApiControllerBase
    {
        private readonly IRequestHandler<CreateTokenCommand, TokenObjectModel> _handler;

        /// <summary>
        /// Creates a new instance of CreateTokenController
        /// </summary>
        /// <param name="handler">Command handler.</param>
        /// <param name="logger">Loggerr implementation</param>
        public CreateTokenController(IRequestHandler<CreateTokenCommand, TokenObjectModel> handler, ILogger<ApiControllerBase> logger) : base(logger)
        {
            _handler = handler;
        }

        /// <summary>
        /// Creates a new token if operation succeeds
        /// </summary>
        /// <param name="command">Token request parameters wrapper</param>
        /// <param name="cancellationToken">Cancellation token to event to be cancelled.</param>
        /// <returns>If successful, returns an AuthenticationResponseModel</returns>
        [AllowAnonymous]
        [HttpPost("connect/token")]
        public async Task<ActionResult<AuthenticationResponseModel>> CreateToken([FromBody] CreateTokenCommand command, CancellationToken cancellationToken)
        {
            var result = await _handler.HandleAsync(command, cancellationToken);
            return new ActionResult<AuthenticationResponseModel>(new AuthenticationResponseModel
            {
                Email = result.Email,
                AccessToken = result.AccessToken
            });
        }
    }
}
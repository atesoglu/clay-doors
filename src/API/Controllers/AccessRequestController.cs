using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers.Base;
using Application.Flows.Access.Commands;
using Application.Models.Access;
using Application.Request;
using Domain.Models.Access;
using Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    /// <summary>
    /// AccessGrant request API
    /// </summary>
    public class AccessRequestController : ApiControllerBase
    {
        private readonly IRequestHandler<AccessRequestCommand, AccessGrantObjectModel> _handler;

        /// <summary>
        /// Creates a new instance of AccessRequestController
        /// </summary>
        /// <param name="handler">Command handler.</param>
        /// <param name="logger">Loggerr implementation</param>
        public AccessRequestController(IRequestHandler<AccessRequestCommand, AccessGrantObjectModel> handler, ILogger<ApiControllerBase> logger) : base(logger)
        {
            _handler = handler;
        }

        /// <summary>
        /// Grants access if operation succeeds
        /// </summary>
        /// <param name="command">AccessRequestCommand request parameters wrapper</param>
        /// <param name="cancellationToken">Cancellation token to event to be cancelled.</param>
        /// <returns>If successful, returns an AccessGrantObjectModel</returns>
        [HttpPost("request-access")]
        public async Task<ActionResult<IResponseModel<AccessGrantObjectModel>>> AccessRequest([FromBody] AccessRequestCommand command, CancellationToken cancellationToken)
        {
            command.Email = User.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Email)?.Value;

            return new ActionResult<IResponseModel<AccessGrantObjectModel>>(new ResponseModel<AccessGrantObjectModel>
            {
                Message = $"Access requested for check-point: {command.Address}.",
                Data = await _handler.HandleAsync(command, cancellationToken),
                Total = 1
            });
        }
    }
}
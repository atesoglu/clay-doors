using System.Text.Json;
using Application.Models.Access;
using Application.Request;
using Domain.Models.Access;

namespace Application.Flows.Access.Commands
{
    /// <summary>
    /// AccessRequestCommand request
    /// </summary>
    public class AccessRequestCommand : Request<AccessGrantObjectModel>
    {
        /// <summary>
        /// Email address for the request user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Address of the checkpoint
        /// </summary>
        public string Address { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
using System.Text.Json;
using Application.Models.Authentication;
using Application.Request;

namespace Application.Flows.Authentication.Commands
{
    /// <summary>
    /// CreateTokenCommand request
    /// </summary>
    public class CreateTokenCommand : Request<TokenObjectModel>
    {
        /// <summary>
        /// Email address to define user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
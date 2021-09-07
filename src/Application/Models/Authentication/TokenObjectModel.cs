using System;
using System.Text.Json;
using Application.Models.Base;

namespace Application.Models.Authentication
{
    public class TokenObjectModel : ObjectModelBase
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public int Expires { get; set; }
        public DateTimeOffset RefreshTokenExpiresAt { get; set; }
        public string Scope { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
namespace Application.Models.Authentication
{
    /// <summary>
    /// Token configuration entity to be assigned from json configuration source.
    /// </summary>
    public class TokenConfigModel
    {
        /// <summary>
        /// JWT authentication secret is the key using which we are going to sign the Token.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The Issuer issues this token to the user after he is authenticated.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The intended user of the token.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Time in minutes and used to set the token expiration time.
        /// </summary>
        public int AccessTokenExpiration { get; set; }

        /// <summary>
        /// Time in minutes and used to set the refresh token expiration.
        /// </summary>
        public int RefreshTokenExpiration { get; set; }

        /// <summary>
        /// Creates a new <see cref="TokenConfigModel"/>.
        /// </summary>
        public TokenConfigModel()
        {
            Secret = "fc58ff8e-bac4-4dfb-94b6-59c3cbc226cb";
            Issuer = "http://localhost:5000";
            Audience = "http://localhost:5000";
            AccessTokenExpiration = 20;
            RefreshTokenExpiration = 180;
        }
    }
}
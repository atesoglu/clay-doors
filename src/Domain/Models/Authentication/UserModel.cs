using System.Collections.Generic;
using System.Text.Json;
using Domain.Models.Access;
using Domain.Models.Base;

namespace Domain.Models.Authentication
{
    /// <summary>
    /// User domain entity.
    /// </summary>
    public class UserModel : ModelBase
    {
        /// <summary>
        /// Primary key of the User entity.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Email address.
        /// Required.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Salt for password hashing.
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// Hashed password.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Claims collection.
        /// </summary>
        public ICollection<ClaimModel> Claims { get; set; }
        /// <summary>
        /// Given access grants for the User
        /// </summary>
        public ICollection<AccessGrantModel> AccessGrants { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
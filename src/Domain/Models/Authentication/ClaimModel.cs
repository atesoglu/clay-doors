using System.Text.Json;
using Domain.Models.Base;

namespace Domain.Models.Authentication
{
    /// <summary>
    /// User claim domain entity.
    /// </summary>
    public class ClaimModel : ModelBase
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int ClaimId { get; set; }
        /// <summary>
        /// UserId which claim belongs to.
        /// Required.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Claim type.
        /// Required.
        /// </summary>
        public string ClaimType { get; set; }
        /// <summary>
        /// Claim value.
        /// Required.
        /// </summary>
        public string ClaimValue { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
using System.Text.Json;
using Application.Models.Base;
using Domain.Models.Authentication;

namespace Application.Models.Authentication
{
    /// <summary>
    /// Claim DTO model
    /// </summary>
    public class ClaimObjectModel : ObjectModelBase<ClaimModel>
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int ClaimId { get; set; }
        /// <summary>
        /// UserId which claim belongs to
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Claim type
        /// </summary>
        public string ClaimType { get; set; }
        /// <summary>
        /// Claim value
        /// </summary>
        public string ClaimValue { get; set; }

        /// <summary>
        /// Creates a new instance of CheckPointObjectModel
        /// </summary>
        public ClaimObjectModel()
        {
        }

        /// <summary>
        /// Creates a new instance of CheckPointObjectModel
        /// </summary>
        /// <param name="entity">Domain entity to read and assign the values</param>
        public ClaimObjectModel(ClaimModel entity)
        {
            AssignFrom(entity);
        }

        /// <summary>
        /// Assign domain entity properties to respective fields.
        /// </summary>
        /// <param name="entity">Domain entity to read and assign the values.</param>
        public sealed override void AssignFrom(ClaimModel entity)
        {
            ClaimId = entity.ClaimId;
            UserId = entity.UserId;
            ClaimType = entity.ClaimType;
            ClaimValue = entity.ClaimValue;
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
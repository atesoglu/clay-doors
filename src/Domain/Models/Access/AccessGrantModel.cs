using System.Text.Json;
using Domain.Models.Authentication;
using Domain.Models.Base;
using Domain.Models.CheckPoints;

namespace Domain.Models.Access
{
    /// <summary>
    /// Access grant domain entity.
    /// </summary>
    public class AccessGrantModel : ModelBase
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int AccessGrantId { get; set; }

        /// <summary>
        /// CheckPointId which grant belongs to.
        /// </summary>
        public int CheckPointId { get; set; }

        /// <summary>
        /// UserId which grant belongs to.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// CheckPoint domain entity reference.
        /// </summary>
        public CheckPointModel CheckPoint { get; set; }

        /// <summary>
        /// User domain entity reference.
        /// </summary>
        public UserModel User { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
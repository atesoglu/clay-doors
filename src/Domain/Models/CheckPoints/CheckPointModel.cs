using System.Collections.Generic;
using System.Text.Json;
using Domain.Models.Access;
using Domain.Models.Base;

namespace Domain.Models.CheckPoints
{
    /// <summary>
    /// Checkpoint (sometimes as doors) domain entity
    /// </summary>
    public class CheckPointModel : ModelBase
    {
        /// <summary>
        /// Primary key of CheckPoint entity.
        /// Required.
        /// </summary>
        public int CheckPointId { get; set; }
        /// <summary>
        /// Address of the CheckPoint.
        /// Required.
        /// </summary>
        public string Address { get; set; }

        // public ICollection<CheckPointTagModel> AllowedAccesses { get; set; }
        
        /// <summary>
        /// Given access grants for the CheckPoint
        /// </summary>
        public ICollection<AccessGrantModel> AccessGrants { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
using System;
using System.Text.Json;
using Application.Models.Base;
using Domain.Models.Access;

namespace Application.Models.Access
{
    /// <summary>
    /// AccessGrant DTO model
    /// </summary>
    public class AccessGrantObjectModel : ObjectModelBase<AccessGrantModel>
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

        public AccessGrantObjectModel()
        {
        }

        public AccessGrantObjectModel(AccessGrantModel entity)
        {
            AssignFrom(entity);
        }

        public sealed override void AssignFrom(AccessGrantModel entity)
        {
            AccessGrantId = entity.AccessGrantId;
            CheckPointId = entity.CheckPointId;
            UserId = entity.UserId;
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
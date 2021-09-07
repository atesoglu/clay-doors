using System;
using Application.Events.Base;
using Application.Models.Access;

namespace Application.Events
{
    /// <summary>
    /// Event wrapper for AccessGrantObjectModel
    /// </summary>
    public class AccessGrantCreatedEvent : Event<AccessGrantObjectModel>
    {
        /// <summary>
        /// Creates a new instance of AccessGrantCreatedEvent
        /// </summary>
        /// <param name="model">Actual DTO</param>
        /// <param name="requestedAt">Request timestamp</param>
        public AccessGrantCreatedEvent(AccessGrantObjectModel model, DateTimeOffset requestedAt) : base(model, requestedAt)
        {
        }
    }
}
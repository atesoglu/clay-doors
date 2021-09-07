using System;
using Application.Events.Base;
using Application.Models.Authentication;
using Application.Models.CheckPoints;

namespace Application.Events
{
    /// <summary>
    /// Event wrapper for TokenObjectModel
    /// </summary>
    public class TokenCreatedEvent : Event<TokenObjectModel>
    {
        /// <summary>
        /// Creates a new instance of TokenCreatedEvent
        /// </summary>
        /// <param name="model">Actual DTO</param>
        /// <param name="requestedAt">Request timestamp</param>
        public TokenCreatedEvent(TokenObjectModel model, DateTimeOffset requestedAt) : base(model, requestedAt)
        {
        }
    }
}
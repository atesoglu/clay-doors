using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.Attributes
{
    /// <summary>
    /// Identifies an action that supports the HTTP PURGE method.
    /// </summary>
    public class HttpPurgeAttribute : HttpMethodAttribute
    {
        private static readonly IEnumerable<string> SupportedMethods = new[] { "PURGE" };

        /// <summary>
        /// Creates a new <see cref="HttpPurgeAttribute"/>.
        /// </summary>
        public HttpPurgeAttribute() : base(SupportedMethods)
        {
        }

        /// <summary>
        /// Creates a new <see cref="HttpPurgeAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public HttpPurgeAttribute(string template) : base(SupportedMethods, template)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));
        }
    }
}
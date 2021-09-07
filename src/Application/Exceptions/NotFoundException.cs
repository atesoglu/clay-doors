using System;
using Application.Exceptions.Base;
using Microsoft.Extensions.Logging;

namespace Application.Exceptions
{
    /// <summary>
    /// Not found exception
    /// </summary>
    public class NotFoundException : ExceptionBase
    {
        /// <summary>
        /// Creates a new instance of InternalServerException
        /// </summary>
        public NotFoundException() : base("The request could not be processed because of conflict in the request, such as the requested resource is not in the expected state, " +
                                          "or the result of processing the request would create a conflict within the resource.")
        {
        }

        /// <summary>
        /// Creates a new instance of NotFoundException
        /// </summary>
        /// <param name="message">Exception message</param>
        public NotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of NotFoundException
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Generic inner exception</param>
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
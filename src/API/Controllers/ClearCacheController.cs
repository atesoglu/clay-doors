using API.Attributes;
using API.Controllers.Base;
using Application.Cache;
using Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    /// <summary>
    /// Cache purging API
    /// </summary>
    public class ClearCacheController : ApiControllerBase
    {
        /// <summary>
        /// Creates a new instance of ClearCacheController
        /// </summary>
        /// <param name="logger">Loggerr implementation</param>
        public ClearCacheController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }

        /// <summary>
        /// Purges the caches
        /// </summary>
        /// <returns>If successful, cache expires</returns>
        [HttpPurge("cache")]
        public ActionResult<IResponseModel> ClearCache()
        {
            AccessGrantTokenSourceProvider.ResetCancellationToken();

            return new ActionResult<IResponseModel>(new ResponseModel { Message = "Cache cleared successfully." });
        }
    }
}
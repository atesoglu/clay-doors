using System.Threading;
using Microsoft.Extensions.Primitives;

namespace Application.Cache
{
    /// <summary>
    /// Token source provider for AccessGrantModel
    /// </summary>
    public static class AccessGrantTokenSourceProvider
    {
        private static CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private static CancellationChangeToken _changeToken;

        /// <summary>
        /// Gets a new cancellation token to be used in cache operations.
        /// </summary>
        /// <returns>Returns a new CancellationChangeToken</returns>
        public static IChangeToken GetCancellationToken()
        {
            return _changeToken ??= new CancellationChangeToken(_tokenSource.Token);
        }

        /// <summary>
        /// Resets the cancellation token and purges caches.
        /// </summary>
        public static void ResetCancellationToken()
        {
            if (_tokenSource is { IsCancellationRequested: false, Token: { CanBeCanceled: true } })
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }

            _tokenSource = new CancellationTokenSource();
            _changeToken = null;
        }
    }
}
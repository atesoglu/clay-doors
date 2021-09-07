using System.Threading;
using System.Threading.Tasks;
using Domain.Models.Access;
using Domain.Models.Authentication;
using Domain.Models.CheckPoints;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence
{
    /// <summary>
    /// Generic repository implementation supporting EF core.
    /// </summary>
    public interface IDataContext
    {
        DbSet<UserModel> Users { get; set; }
        DbSet<ClaimModel> Claims { get; set; }
        DbSet<CheckPointModel> CheckPoints { get; set; }
        DbSet<AccessGrantModel> AccessGrants { get; set; }
        /*DbSet<TagModel> Tags { get; set; }
        DbSet<CheckPointTagModel> CheckPointTags { get; set; }*/

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the action.</param>
        /// <returns>Task wrapping modified entity count.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
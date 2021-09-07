using System.Collections.Generic;
using System.Threading;
using Application.Persistence;
using Domain.Models.Access;
using Domain.Models.Authentication;
using Domain.Models.CheckPoints;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// In-memory database extensions.
    /// </summary>
    public static class InMemoryDataContextExtensions
    {
        /// <summary>
        /// Seeds the in-memory database with required entities.
        /// </summary>
        /// <param name="dbContext">IDataContext instance to be seeded.</param>
        public static void SeedData(this IDataContext dbContext)
        {
            dbContext.Users.AddRange(new List<UserModel>
            {
                new UserModel { Email = "user1@domain.com", Salt = "salt", PasswordHash = "ZTzIiNk37+IoEKXL2yWlvYLi67J6gA+Fz6NgptklGY4=" },
                new UserModel { Email = "user2@domain.com", Salt = "salt", PasswordHash = "ZTzIiNk37+IoEKXL2yWlvYLi67J6gA+Fz6NgptklGY4=" },
            });

            dbContext.CheckPoints.AddRange(new List<CheckPointModel>
            {
                new CheckPointModel { Address = "Tunnel Door" },
                new CheckPointModel { Address = "Office Door" },
            });

            dbContext.AccessGrants.AddRange(new List<AccessGrantModel>
            {
                new AccessGrantModel { UserId = 1, CheckPointId = 1 },
                new AccessGrantModel { UserId = 1, CheckPointId = 2 },
                new AccessGrantModel { UserId = 2, CheckPointId = 1 },
            });

            dbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
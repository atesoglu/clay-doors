using Application.Persistence;
using Domain.Models.Access;
using Domain.Models.Authentication;
using Domain.Models.CheckPoints;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// In-memory implementation of IDataContext which extends DbContext
    /// </summary>
    public class InMemoryDataContext : DbContext, IDataContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ClaimModel> Claims { get; set; }
        public DbSet<CheckPointModel> CheckPoints { get; set; }
        public DbSet<AccessGrantModel> AccessGrants { get; set; }
        /*public DbSet<TagModel> Tags { get; set; }
        public DbSet<CheckPointTagModel> CheckPointTags { get; set; }*/

        public InMemoryDataContext(DbContextOptions<InMemoryDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<UserModel>(entity => entity.HasKey(t => t.UserId))
                .Entity<ClaimModel>(entity => entity.HasKey(t => t.ClaimId))
                .Entity<CheckPointModel>(entity => entity.HasKey(t => t.CheckPointId))
                .Entity<AccessGrantModel>(entity =>
                {
                    entity.HasKey(t => t.AccessGrantId);
                    entity.HasOne(t => t.CheckPoint).WithMany(t => t.AccessGrants).HasForeignKey(d => d.CheckPointId);
                    entity.HasOne(t => t.User).WithMany(t => t.AccessGrants).HasForeignKey(d => d.UserId);
                })
                /*.Entity<TagModel>(entity => entity.HasKey(t => t.TagId))
                .Entity<CheckPointTagModel>(entity => entity.HasKey(t => t.CheckPointTagId))*/
                ;
        }
    }
}
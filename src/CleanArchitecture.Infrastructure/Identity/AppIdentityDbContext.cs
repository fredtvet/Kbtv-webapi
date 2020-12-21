using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using CleanArchitecture.Core;
using CleanArchitecture.SharedKernel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>, IAppIdentityDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<InboundEmailPassword> InboundEmailPasswords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasMany(p => p.Roles).WithOne().HasForeignKey(p => p.UserId).IsRequired();
            builder.Entity<ApplicationUser>().Ignore(p => p.Role).HasQueryFilter(m => m.Deleted == false);
            builder.Entity<InboundEmailPassword>().HasQueryFilter(m => m.Deleted == false);
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var now = DateTimeHelper.Now();
            var user = _currentUserService.UserName;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ITrackable trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedAt = now;
                            trackable.UpdatedBy = user;
                            entry.Property("CreatedBy").IsModified = false;
                            entry.Property("CreatedAt").IsModified = false;
                            break;
                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.UpdatedAt = now;
                            trackable.CreatedBy = user;
                            trackable.Deleted = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            trackable.Deleted = true;
                            trackable.UpdatedAt = now;
                            break;
                    }
                }
            }
        }
    }

}

using System;
using System.Threading;
using System.Threading.Tasks;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Entities.Notification;
using Common.Infrastructures;
using Common.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Extensions;
using Persistence.ValueConverters;
using static Persistence.ValueConverters.LocalizedDataConverter;

namespace Persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
        UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IAppDbContext
    {
        private readonly IAuditService _auditService;

        #region Dbsets
        //Users
        public DbSet<Image> Image { get; set; }

        public DbSet<UserCode> UserCodes { get; set; }

        //Lookups
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        //Ads
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Installment> Installments { get; set; }
        //Services
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<FreeService> FreeServices { get; set; }
        public DbSet<PaidService> PaidServices { get; set; }
        public DbSet<SpaceInfo> SpaceInfos { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AdInterval> AdIntervals { get; set; }
        public DbSet<AdComplaint> AdComplaints { get; set; }
        public DbSet<AdFavourite> AdFavourites { get; set; } 
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; } 

        #endregion


        public AppDbContext(DbContextOptions options, IAuditService auditService = null)
    : base(options)
        {
            _auditService = auditService;

        }


        //public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(256));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(256));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderDisplayName).HasMaxLength(256));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(256));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(256));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(256));
            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                userRole.Property(p => p.RoleId).HasMaxLength(256);
                userRole.Property(p => p.UserId).HasMaxLength(256);

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            builder.UseValueConverterForType<LocalizedData>(new LocalizedDataConverter());
            builder.HasDbFunction(() => CustomDbFunctions.JsonValue(default, default));
            builder.Entity<City>(city =>
            {

                city.HasOne(ur => ur.Country)
                          .WithMany(r => r.Cities)
                          .HasForeignKey(ur => ur.CountryId)
                          .IsRequired();

            });
        }

        public async Task<IDbContextTransaction> CreateTransaction()
        {
            return await this.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            this.Database.CommitTransaction();
        }

        public void Rollback()
        {
            this.Database.RollbackTransaction();
        }


        #region overrides

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            if (entity is IAudit audit)
            {
                audit.CreatedBy = _auditService.UserName;
                audit.CreatedDate = DateTime.UtcNow;
            }
            return base.Add(entity);
        }
        public override EntityEntry Add(object entity)
        {
            if (entity is IAudit audit)
            {
                audit.CreatedBy = _auditService.UserName;
                audit.CreatedDate = DateTime.UtcNow;
            }
            return base.Add(entity);
        }
        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            if (entity is IAudit audit)
            {
                audit.CreatedBy = _auditService.UserName;
                audit.CreatedDate = DateTime.UtcNow;
            }
            return base.AddAsync(entity, cancellationToken);
        }
        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is IAudit audit)
            {
                audit.CreatedBy = _auditService.UserName;
                audit.CreatedDate = DateTime.UtcNow;
            }
            return base.AddAsync(entity, cancellationToken);
        }

        public override EntityEntry Update(object entity)
        {
            if (entity is IAudit audit)
            {
                audit.UpdatedBy = _auditService.UserName;
                audit.UpdatedDate = DateTime.UtcNow;
            }
            return base.Update(entity);
        }
        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            if (entity is IAudit audit)
            {
                audit.UpdatedBy = _auditService.UserName;
                audit.UpdatedDate = DateTime.UtcNow;
            }
            return base.Update(entity);
        }

        public override EntityEntry Remove(object entity)
        {
            if (entity is IDeleteEntity audit)
            {
                audit.DeletedBy = _auditService.UserName;
                audit.DeletedDate = DateTime.UtcNow;
                audit.IsDeleted = true;
                return Update(entity);
            }
            return base.Remove(entity);
        }
        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            if (entity is IDeleteEntity audit)
            {
                audit.DeletedBy = _auditService.UserName;
                audit.DeletedDate = DateTime.UtcNow;
                audit.IsDeleted = true;
                return Update(entity);
            }
            return base.Remove(entity);
        }

        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync(CancellationToken.None);
        }
    }

}
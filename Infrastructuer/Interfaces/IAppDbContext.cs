using System.Threading;
using System.Threading.Tasks;
using AuthDomain.Entities;
using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using AuthDomain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<T> Set<T>() where T : class;

        DbSet<User> Users { get; set; }


        DbSet<Image> Image { get; set; }

        DbSet<UserCode> UserCodes { get; set; }

        //Lookups
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        //Ads
        DbSet<Ad> Ads { get; set; }
        DbSet<AdInterval> AdIntervals { get; set; }
        DbSet<SpaceInfo> SpaceInfos { get; set; }
        DbSet<AdComplaint> AdComplaints { get; set; }

        DbSet<Transaction> Transactions { get; set; }
        DbSet<Installment> Installments { get; set; }
        DbSet<Auction> Auctions { get; set; }

        //Services
        DbSet<Service> Services { get; set; }
        DbSet<ServiceType> ServiceTypes { get; set; }
        DbSet<FreeService> FreeServices { get; set; }
      //  DbSet<Notification> Notifications { get; set; }

        DbSet<PaidService> PaidServices { get; set; }
        DbSet<AdFavourite> AdFavourites { get; set; } 
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<IDbContextTransaction> CreateTransaction();
        void Commit();
        void Rollback();
    }
}
using Common.Interfaces;
using Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.MSSQL;
using AuthDomain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Persistence.MYSQL;
using Web.Globals;

namespace Web.Extensions {
  public class MainDbRegister : IInstaller {
    public void InstallService(IServiceCollection services, IConfiguration configuration) {
      var persistenceConfig = configuration?.GetSection(nameof(Sections.Persistence))?.Get<PersistenceConfiguration>();

      if (persistenceConfig?.Provider == "MSSQL") {
        services.AddMssqlDbContext(configuration);
        //services.AddDbContext<AppDbContext, MsSqlAppDbContext>();
      }
      if (persistenceConfig?.Provider == "MYSQL") {
        //services.AddMySqlDbContext(configuration);
        services.AddMysqlDbContext(configuration);
      }

      services
     .AddIdentity<User, Role>()
     .AddEntityFrameworkStores<AppDbContext>()
     .AddDefaultTokenProviders();
    }
  }

  //public static class MsSqlServiceCollectionExtensions {
  //  public static IServiceCollection AddMssqlDbContext(
  //      this IServiceCollection services,
  //      IConfiguration config = null) {
  //    services.AddDbContext<AppDbContext, MsSqlAppDbContext>(options => {
  //      options.UseSqlServer(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Persistence.MSSQL"));
  //    });
  //    services.AddIdentity<User, Role>()
  //            .AddEntityFrameworkStores<MsSqlAppDbContext>()
  //            .AddDefaultTokenProviders();
  //    return services;
  //  }
  //}
  //public static class MySqlServiceCollectionExtensions {
  //  public static IServiceCollection AddMySqlDbContext(
  //      this IServiceCollection services,
  //      IConfiguration config = null) {
  //    services.AddDbContext<AppDbContext, MsSqlAppDbContext>(options => {
  //      options.UseMySQL(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Persistence.MYSQL"));
  //    });

  //    services.AddIdentity<User, Role>()
  //      .AddEntityFrameworkStores<MsSqlAppDbContext>()
  //      .AddDefaultTokenProviders();
  //    return services;
  //  }
  //}
}
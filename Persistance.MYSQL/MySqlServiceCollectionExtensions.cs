using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.MYSQL {
  public static class MySqlServiceCollectionExtensions {
    public static IServiceCollection AddMysqlDbContext(
        this IServiceCollection serviceCollection,
        IConfiguration config = null) {


      serviceCollection.AddDbContext<AppDbContext, MySqlAppDbContext>(options => {
        options.UseMySQL(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(MySqlAppDbContext).Assembly.FullName));
      });
      return serviceCollection;
    }
  }
}

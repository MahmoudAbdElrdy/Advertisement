using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.MYSQL {
  public class MySqlContextFactory : IDesignTimeDbContextFactory<MySqlAppDbContext> {
    public MySqlAppDbContext CreateDbContext(string[] args) {
      var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", true)
          .AddJsonFile("appsettings.Development.json", false)
          .Build();

      var builder = new DbContextOptionsBuilder<AppDbContext>();

      var connectionString = config.GetConnectionString("DefaultConnection");
      builder.UseMySQL(connectionString, c => c.MigrationsAssembly("Persistence.MYSQL"));
      return new MySqlAppDbContext(builder.Options, null);
    }
  }
}

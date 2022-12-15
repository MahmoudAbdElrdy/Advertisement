using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Web {
  public class Program {
    public static void Main(string[] args) {
      var config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
      //Initialize Logger
      Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(config)
          .Enrich.FromLogContext()
          .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
          .Enrich.WithProcessId()
          //.Enrich.WithEnvironmentUserName()
          .CreateLogger();
      try {
        Log.Information("Application Starting.");
        CreateHostBuilder(args).Build().Run();
      } catch (Exception ex) {
        Log.Fatal(ex, "The Application failed to start.");
      } finally {
        Log.CloseAndFlush();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
      .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder => {
          webBuilder.UseStartup<Startup>();
        });
  }
}
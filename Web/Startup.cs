using Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using System;
using System.Linq;
using AutoMapper;
using Common.Extensions;
using FluentValidation.AspNetCore;
using AuthApplication.Auth.Validatores;
using Web.Extensions;
using Serilog;
using Common.Options;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Web.Middleware;

using System.ComponentModel;

namespace Web {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddControllersWithViews();
      AppSettings appSettings = new AppSettings();
      Configuration.Bind("SystemSetting", appSettings);
      services.AddSingleton(appSettings);

      var installers = typeof(Startup).Assembly.ExportedTypes.Where(a =>
          typeof(IInstaller).IsAssignableFrom(a) && !a.IsInterface && !a.IsAbstract)
        .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();


      installers.ForEach(i => i.InstallService(services, Configuration));

      if (services != null) {
        var provider = services.BuildServiceProvider();

        BaseEntityExtension.Configure(provider.GetService<IMapper>());
        provider.GetRequiredService<IServiceScopeFactory>();
      }

      services.AddMvc().AddFluentValidation(confg => {
        confg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
        confg.RegisterValidatorsFromAssemblyContaining<LoginValidator>();
      });
      // In production, the React files will be served from this directory
      services.AddSpaStaticFiles(configuration => {
        configuration.RootPath = "ClientApp/dist";
      });
          
            #region CorsPolicy

            services.AddCors(options => {
        options.AddPolicy("CorsPolicy",
          builder => builder.AllowAnyOrigin()
                  //  builder => builder.WithOrigins("http://assemebeed-001-site1.itempurl.com", "http://assemobeid40-001-site1.ftempurl.com")

            .AllowAnyMethod()
            .AllowAnyHeader());
      });

      #endregion
    }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext ctx) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseCors("CorsPolicy");
      // app.UseHttpsRedirection();
      app.UseStaticFiles();
      //app.UseSpaStaticFiles();

      app.UseRouting();


      app.UseAuthorization()
        .UseCustomExceptionHandler().UseMiddleware<ImageProxyMiddleware>()
        .UseSwagger()
        .UseSwaggerUI(c => {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
          c.RoutePrefix = "api";
        })
        //.UseSerilogRequestLogging(cfg=>cfg)
        ;

      app.UseEndpoints(endpoints => {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa => {
        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment()) {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });

      ctx.Database.Migrate();
      AppDbInitializer.Initialize(ctx);
    }
  }
}
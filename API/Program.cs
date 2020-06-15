using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.AutoFac;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            // using(var scope=host.Services.CreateScope())
            // {
            //     var services=scope.ServiceProvider;
            //      var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //     try
            //     {
            //         var context=services.GetRequiredService<DataContext>();
            //         await context.Database.MigrateAsync();
            //         await SeedContext.SeedAsync(context,loggerFactory);
                    
            //     }
            //     catch (Exception ex)
            //     {
            //         var logger=services.GetRequiredService<ILogger<Program>>();
            //         logger.LogError(ex,"An Error occured during migrations");
            //     }
            // }

           

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder=>{
                    builder.RegisterModule(new AutoFacBusinessModule());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

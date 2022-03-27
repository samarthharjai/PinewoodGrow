using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PinewoodGrow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var identityContext = services.GetRequiredService<ApplicationDbContext>();
                    identityContext.Database.Migrate();
                    ApplicationSeedData.SeedAsync(identityContext, services).Wait();
                    var context = services.GetRequiredService<GROWContext>();
                    context.Database.Migrate();
                     GSeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
//_message="An item with the same key has already been added. Key: ChIJS3EXEzFD04kRxOg93TUaJBI"
//Message="An item with the same key has already been added. Key: ChIJ4b6H3DND04kR7As4iJJThxM"
//ChIJdcUvjjFD04kRq6g4mn5ujvU
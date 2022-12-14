using System;
using Booking.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Booking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Migrate/Create the database if it doesn't exists
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<BookingDbContext>();

                    var concreteContext = (BookingDbContext)context;

                    //We are using Linux SQL container and it may take some time to fireup .
                    //We are retrying the connectivity
                    Policy
                       .Handle<Exception>()
                       .WaitAndRetry(5, r => TimeSpan.FromSeconds(10))
                       .Execute(() => concreteContext.Database.Migrate());
                }
                catch (Exception)
                {

                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>();
    }
}

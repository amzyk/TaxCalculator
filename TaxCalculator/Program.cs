using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace TaxCalculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .AddJsonFile("tax.json", optional: false)
                 .Build();

            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("tax.json", optional: false)
                    .AddEnvironmentVariables()
                    .Build();

            return WebHost
                    .CreateDefaultBuilder(args)
                    .UseConfiguration(config)
                    .UseStartup<Startup>();
        }
    }
}

using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using TaxCalculator.Configuration;
using TaxCalculator.Services;

namespace TaxCalculator
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddMvcOptions(o => o.EnableEndpointRouting = false)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TaxCalculationService>());

            services
                .Configure<Tax>(options => Configuration.GetSection("Tax").Bind(options));
            services
                .Configure<Insurance>(options => Configuration.GetSection("Insurance").Bind(options));
            services
                .Configure<Reduction>(options => Configuration.GetSection("Reduction").Bind(options));

            services.AddTransient<FirstTaxStageCalculationService>();
            services.AddTransient<SecondTaxStageCalculationService>();
            services.AddTransient<ThirdTaxStageCalculationService>();

            services.AddTransient<Func<decimal, Tax, ITaxCalculationService>>(serviceProvider => (salaryValue, taxConfig) =>
            {
                if (taxConfig == null)
                {
                    throw new ArgumentNullException();
                }

                if (salaryValue > taxConfig.ThirdStage.Step)
                    return serviceProvider.GetService<ThirdTaxStageCalculationService>();

                if (salaryValue > taxConfig.SecondStage.Step)
                    return serviceProvider.GetService<SecondTaxStageCalculationService>();

                return serviceProvider.GetService<FirstTaxStageCalculationService>();
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}

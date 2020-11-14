using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Wavect.TariffComparison.GlobalConstants;
using Wavect.TariffComparison.Persistence.Context;
using Wavect.TariffComparison.WebAPI.Extensions;

namespace Wavect.TariffComparison.WebAPI
{
    public class Startup
    {
        #region constants
        private const string _dbContext = "TariffComparisonContext";
        #endregion

        #region properties
        public IConfiguration Configuration { get; }
        #endregion

        #region ctor
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        #endregion

        #region methods_public
        /// <summary>
        /// Configures WebApi Services, ...
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(c =>
            {
                // Add global route prefix (e.g. /api)
                c.UseCentralRoutePrefix(
                    new RouteAttribute(
                        Configuration.GetValue<string>("Api:GlobalRoutePrefix")));
            });

#if DEBUG
            // Add swagger open api documentation
            services.AddSwaggerGenerator();
#endif

            // Add database connection
            services.AddDbContext<TariffComparisonContext>(
              options => options.UseSqlServer(Configuration.GetConnectionString(_dbContext)));

            // Enable ApiVersioning
            services.AddApiVersioning(v =>
            {
                v.DefaultApiVersion = new ApiVersion(ApiConfiguration.LatestMajorApiVersion, ApiConfiguration.LatestMinorApiVersion);
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.ReportApiVersions = true;
            });
        }

        /// <summary>
        /// Setting up webApi in corresponding environment.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

#if DEBUG
            app.AddSwaggerEndpoint();
#endif

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #endregion
    }
}

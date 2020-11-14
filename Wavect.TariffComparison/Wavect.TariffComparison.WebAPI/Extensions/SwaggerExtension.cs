using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wavect.TariffComparison.GlobalConstants;

namespace Wavect.TariffComparison.WebAPI.Extensions
{
    /// <summary>
    /// Used for cleaner code & separation of concerns.
    /// </summary>
    public static class SwaggerExtension
    {
        #region constants
        /// <summary>
        /// Just to be consistent
        /// </summary>
        private const string SwaggerTitle = "Wavect - TariffComparison";
        #endregion

        #region methods_public

        /// <summary>
        /// Generate swagger openApi document to show it via /swagger route.
        /// </summary>
        public static IServiceCollection AddSwaggerGenerator(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{ApiConfiguration.LATEST_API_VERSION}", new OpenApiInfo
                {
                    Title = SwaggerTitle,
                    Version = $"v{ApiConfiguration.LATEST_API_VERSION}",
                });
            });

            return services;
        }

        /// <summary>
        /// Adds swagger endpoint to show a nice overview of all api-routes.
        /// </summary>
        public static IApplicationBuilder AddSwaggerEndpoint(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{ApiConfiguration.LATEST_API_VERSION}/swagger.json", SwaggerTitle);
            });

            return app;
        }
        #endregion
    }
}

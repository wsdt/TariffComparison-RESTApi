using System;
using System.Collections.Generic;
using System.Text;

namespace Wavect.TariffComparison.DTOs.ApiVersionNeutral.Responses
{
    /// <summary>
    /// Used to provide general information about the api.
    /// </summary>
    public class ApiInfoDTO : ApiVersionNeutralBaseDTO
    {
        /// <summary>
        /// Indicates latest available ApiVersion.
        /// </summary>
        public string LatestApiVersion { get; set; }

        /// <summary>
        /// Indicates if runtime checks succeed.
        /// Called by e.g. Docker via HealthCheck.
        /// </summary>
        public bool IsApiHealthy { get; set; }
    }
}

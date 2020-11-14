using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wavect.TariffComparison.DTOs.ApiVersionNeutral.Responses;
using Wavect.TariffComparison.GlobalConstants;
using Wavect.TariffComparison.Persistence.Context;
using Wavect.TariffComparison.Services.ApiVersionNeutral.Interfaces;

namespace Wavect.TariffComparison.Services.ApiVersionNeutral
{
    public class ApiInfoService : IApiInfoService
    {
        #region fields
        private TariffComparisonContext _dbContext;
        #endregion

        #region ctor
        public ApiInfoService(TariffComparisonContext dbContext) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        #endregion

        #region GET
        public async Task<ApiInfoDTO> Get()
        {
            return new ApiInfoDTO() { 
                IsApiHealthy = await evaluateApiHealth(),
                LatestApiVersion = ApiConfiguration.LATEST_API_VERSION,
            };
        }
        #endregion

        #region methods_private
        private async Task<bool> evaluateApiHealth()
        {
            using (_dbContext)
            {
                return await _dbContext.Database.CanConnectAsync();
            }
        }
        #endregion
    }
}

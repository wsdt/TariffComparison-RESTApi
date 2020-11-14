using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wavect.TariffComparison.DTOs.ApiVersionNeutral.Responses;

namespace Wavect.TariffComparison.Services.ApiVersionNeutral.Interfaces
{
    public interface IApiInfoService
    {
        #region GET
        public Task<ApiInfoDTO> Get();
        #endregion
    }
}

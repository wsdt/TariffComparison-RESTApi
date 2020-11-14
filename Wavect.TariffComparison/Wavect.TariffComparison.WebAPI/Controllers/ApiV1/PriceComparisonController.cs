using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wavect.TariffComparison.DTOs.ApiV1;
using Wavect.TariffComparison.GlobalConstants;
using Wavect.TariffComparison.Services.ApiV1.Interfaces;

namespace Wavect.TariffComparison.WebAPI.Controllers.ApiV1
{
    [ApiController]
    [ApiVersion(ApiConfiguration.API_VERSION_v1_0)]
    [Route("v{version:apiVersion}/compare")]
    public class PriceComparisonController : ControllerBase
    {
        #region fields
        private readonly ILogger<PriceComparisonController> _logger;
        private readonly IPriceComparisonService _priceComparisonService;
        #endregion

        #region ctor
        public PriceComparisonController(ILogger<PriceComparisonController> logger, IPriceComparisonService priceComparisonService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _priceComparisonService = priceComparisonService ?? throw new ArgumentNullException(nameof(priceComparisonService));
        }
        #endregion

        #region GET
        [HttpGet("{consumptionKWh}")]
        public async Task<IActionResult> GetCheapestFor(decimal consumptionKWh)
        {
            _logger.LogDebug("GetCheapestFor called..");
            return Ok(await _priceComparisonService.GetCheapestFor(consumptionKWh));
        }
        #endregion
    }
}

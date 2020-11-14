using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wavect.TariffComparison.DTOs.ApiVersionNeutral;
using Wavect.TariffComparison.Services.ApiVersionNeutral.Interfaces;

namespace Wavect.TariffComparison.WebAPI.Controllers.ApiVersionNeutral
{
    [ApiController]
    [ApiVersionNeutral]
    [Route("info")]
    public class ApiInfoController : ControllerBase
    {

        private readonly ILogger<ApiInfoController> _logger;
        private readonly IApiInfoService _apiInfoService;

        public ApiInfoController(ILogger<ApiInfoController> logger, 
            IApiInfoService apiInfoService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _apiInfoService = apiInfoService ?? throw new ArgumentNullException(nameof(apiInfoService));
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug("GetApiInfo called..");
            return Ok(await _apiInfoService.Get());
        }
    }
}

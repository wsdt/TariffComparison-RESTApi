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
    [Route("v{version:apiVersion}/tariff")]
    public class TariffController : ControllerBase
    {
        #region fields
        private readonly ILogger<TariffController> _logger;
        private readonly ITariffService _tariffService;
        #endregion

        #region ctor
        public TariffController(ILogger<TariffController> logger, ITariffService tariffService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tariffService = tariffService ?? throw new ArgumentNullException(nameof(tariffService));
        }
        #endregion

        #region GET
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogDebug("GetTariffById called..");
            TariffDTO tariff = await _tariffService.Get(id);
            if (tariff == null)
            {
                return NotFound();
            }
            return Ok(tariff);
        }

        [HttpGet()]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("GetAllTariffs called..");
            return Ok(await _tariffService.GetAll());
        }
        #endregion
    }
}

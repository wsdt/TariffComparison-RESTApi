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
    [Route("v{version:apiVersion}/product")]
    public class ProductController : ControllerBase
    {
        #region fields
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        #endregion

        #region ctor
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        #endregion

        #region GET
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogDebug("GetProductById called..");
            ProductDTO product = await _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet()]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("GetAllProducts called..");
            return Ok(await _productService.GetAll());
        }
        #endregion
    }
}

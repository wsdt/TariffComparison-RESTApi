using System;
using System.Collections.Generic;
using System.Text;

namespace Wavect.TariffComparison.DTOs.ApiV1
{
    public class ProductDTO : ApiV1BaseDTO
    {
        /// <summary>
        /// Non-translated display name of tariff.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cheapest pricing model, if it doesn't comply with
        /// current usage the next more expensive pricing model is used.
        /// </summary>
        public TariffDTO InitialTariff { get; set; }
    }
}

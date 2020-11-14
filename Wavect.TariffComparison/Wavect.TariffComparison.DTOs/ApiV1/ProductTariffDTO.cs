using System;
using System.Collections.Generic;
using System.Text;

namespace Wavect.TariffComparison.DTOs.ApiV1
{
    public class ProductTariffDTO
    {
        /// <summary>
        /// Id of corresponding product, as name is not unique.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of Tariff
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Calculated estimated annual costs for submitted consumption
        /// </summary>
        public decimal AnnualCosts { get; set; }
    }
}

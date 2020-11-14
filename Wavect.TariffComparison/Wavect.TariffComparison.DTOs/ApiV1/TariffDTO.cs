using System;
using System.Collections.Generic;
using System.Text;

namespace Wavect.TariffComparison.DTOs.ApiV1
{
    public class TariffDTO : ApiV1BaseDTO
    {
        public decimal BaseCostsAnnual { get; set; }

        public decimal ConsumptionCostsPerKWh { get; set; }

        /// <summary>
        /// Limit after which the nextPricingTier is used for consumption
        /// exceeding the current pricing model.
        /// 
        /// If this property is NULL, you can ignore the NextPricingTier property.
        /// </summary>
        public decimal? KWhLimit { get; set; }

        /// <summary>
        /// Next pricingModel is used when KWh which are included
        /// in the tariff are exceeded.
        /// </summary>
        public TariffDTO NextPricingTier { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wavect.TariffComparison.Entities.Persisted
{
    /// <summary>
    /// Decimal used for better precision. 
    /// </summary>
    public class TariffEntity : BaseEntity
    {
        [Column(TypeName = "decimal(25,3)")]
        public decimal BaseCostsAnnual { get; set; }

        [Column(TypeName = "decimal(25,3)")]
        public decimal ConsumptionCostsPerKWh { get; set; }

        /// <summary>
        /// Limit after which the nextPricingTier is used for consumption
        /// exceeding the current pricing model.
        /// 
        /// If this property is NULL, you can ignore the NextPricingTier property.
        /// </summary>
        [Column(TypeName = "decimal(18,4)")]
        public decimal? KWhLimit { get; set; }

        /// <summary>
        /// Next pricingModel is used when KWh which are included
        /// in the tariff are exceeded.
        /// </summary>
        public TariffEntity NextPricingTier { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wavect.TariffComparison.Entities.Persisted
{
    public class ProductEntity : BaseEntity
    {
        /// <summary>
        /// Non-translated display name of tariff.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cheapest pricing model, if it doesn't comply with
        /// current usage the next more expensive pricing model is used.
        /// </summary>
        public TariffEntity InitialTariff { get; set; }
    }
}

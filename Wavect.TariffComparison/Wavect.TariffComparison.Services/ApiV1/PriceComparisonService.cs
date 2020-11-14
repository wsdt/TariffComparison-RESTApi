using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wavect.TariffComparison.DTOs.ApiV1;
using Wavect.TariffComparison.Entities.Persisted;
using Wavect.TariffComparison.Persistence.Context;
using Wavect.TariffComparison.Services.ApiV1.Interfaces;

namespace Wavect.TariffComparison.Services.ApiV1
{
    /// <summary>
    /// Comparing operations related to products and services.
    /// </summary>
    public class PriceComparisonService : IPriceComparisonService
    {
        #region fields
        private TariffComparisonContext _dbContext;
        #endregion

        #region ctor
        public PriceComparisonService(TariffComparisonContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        #endregion

        #region GET
        /// <summary>
        /// Calculates the cheapest product considering a specific consumption in kWh.
        /// </summary>
        /// <param name="consumptionKWh">consumption in kWh</param>
        /// <returns>Sorted list of available product tariffs</returns>
        public async Task<List<ProductTariffDTO>> GetCheapestFor(decimal consumptionKWh)
        {
            using (_dbContext)
            {
                List<ProductEntity> products = (await _dbContext.Products
                   .Include(p => p.InitialTariff)
                   .Include(p => p.InitialTariff.NextPricingTier) // also load next pricing tier, but just one (CURRENTLY there is no need for a recursive loading, but data model would allow it)
                   .ToListAsync());

                return products
                    .Adapt<List<ProductTariffDTO>>()
                    .Select((t, i) => { t.AnnualCosts = calculateAnnualCosts(products[i].InitialTariff, consumptionKWh); return t; })
                    .OrderBy(t => t.AnnualCosts)
                    .ToList();
            }
        }
        #endregion

        #region methods_private
        /// <summary>
        /// Recursive calculation. Note that this calculation assumes that lower pricing modells
        /// always just have a baseCost with no consumptionCosts associated. 
        /// </summary>
        /// <param name="tariffEntity"></param>
        /// <param name="consumptionKWh"></param>
        /// <returns></returns>
        private decimal calculateAnnualCosts(TariffEntity tariffEntity, decimal consumptionKWh)
        {
            if (tariffEntity == null)
            {
                // no further pricing tier
                return 0;
            }

            if (tariffEntity.KWhLimit < consumptionKWh)
            {
                // also add current pricing tier costs as next pricing tier calculates from then on (see model 2).
                return tariffEntity.BaseCostsAnnual + calculateAnnualCosts(tariffEntity.NextPricingTier, consumptionKWh - tariffEntity.KWhLimit.Value);
            }
            return tariffEntity.BaseCostsAnnual + tariffEntity.ConsumptionCostsPerKWh * consumptionKWh;
        }
        #endregion
    }
}

using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wavect.TariffComparison.DTOs.ApiV1;
using Wavect.TariffComparison.Entities.Persisted;
using Wavect.TariffComparison.Persistence.Context;
using Wavect.TariffComparison.Services.ApiV1.Interfaces;

namespace Wavect.TariffComparison.Services.ApiV1
{
    public class TariffService : ITariffService
    {
        #region fields
        private TariffComparisonContext _dbContext;
        #endregion

        #region ctor
        public TariffService(TariffComparisonContext dbContext) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        #endregion

        #region GET
        /// <summary>
        /// Fetches a specific tariff with corresponding meta-data (e.g. pricing tier, initial tariff, ...)
        /// </summary>
        /// <param name="id">Product ID to search for</param>
        public async Task<TariffDTO> Get(Guid id)
        {
            using (_dbContext)
            {
                TariffEntity tariff = await _dbContext.Tariffs.FindAsync(id);
                if (tariff != null)
                {
                    // Load additional data (used this way to have the benefits of Find() by still including external data)
                    await _dbContext.Entry(tariff).Reference(t => t.NextPricingTier).LoadAsync();
                   
                    return tariff.Adapt<TariffDTO>();
                }

                return null;
            }
        }

        /// <summary>
        /// Loads all available tariffs.
        /// </summary>
        public async Task<List<TariffDTO>> GetAll()
        {
            using (_dbContext)
            {
                return (await _dbContext.Tariffs
                   .Include(t => t.NextPricingTier) // also load next pricing tier, but just one
                   .ToListAsync()).Adapt<List<TariffDTO>>();
            }
         }
        #endregion
    }
}

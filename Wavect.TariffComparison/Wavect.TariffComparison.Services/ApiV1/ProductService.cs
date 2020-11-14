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
    public class ProductService : IProductService
    {
        #region fields
        private TariffComparisonContext _dbContext;
        #endregion

        #region ctor
        public ProductService(TariffComparisonContext dbContext) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        #endregion

        #region GET
        /// <summary>
        /// Fetches a specific product with corresponding meta-data (e.g. pricing tier, initial tariff, ...)
        /// </summary>
        /// <param name="id">Product ID to search for</param>
        public async Task<ProductDTO> Get(Guid id)
        {
            using (_dbContext)
            {
                ProductEntity product = await _dbContext.Products.FindAsync(id);
                if (product != null)
                {
                    // Load additional data (used this way to have the benefits of Find() by still including external data)
                    await _dbContext.Entry(product).Reference(p => p.InitialTariff).LoadAsync();
                    if (product.InitialTariff != null)
                    {
                        await _dbContext.Entry(product.InitialTariff).Reference(t => t.NextPricingTier).LoadAsync();
                    }

                    return product.Adapt<ProductDTO>();
                }

                return null;
            }
        }

        /// <summary>
        /// Loads all available producs.
        /// </summary>
        public async Task<List<ProductDTO>> GetAll()
        {
            using (_dbContext)
            {
                return (await _dbContext.Products
                   .Include(p => p.InitialTariff)
                   .Include(p => p.InitialTariff.NextPricingTier) // also load next pricing tier, but just one
                   .ToListAsync()).Adapt<List<ProductDTO>>();
            }
         }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wavect.TariffComparison.Entities.Persisted;
using Wavect.TariffComparison.Persistence.Context;

namespace Wavect.TariffComparison.Persistence.Migrations
{
    public class DbInitMigration
    {
        #region methods_public
        public static void Initialize(TariffComparisonContext context)
        {
            context.Database.EnsureCreated();

            // Evaluate if already initialized
            if (context.Products.Any()) {
                return; // db seeded
            }

            initTariffs(context, out IDictionary<string, TariffEntity> dict);
            initProducts(context, dict);
        }
        #endregion

        #region methods_private
        private static void initTariffs(TariffComparisonContext context, out IDictionary<string, TariffEntity> dict)
        {
            // Using dict here and single assignment to have it easier to assign corresponding relationships
            dict = new Dictionary<string, TariffEntity>();

            dict.Add("bet_0", new TariffEntity()
            {
                Id = Guid.NewGuid(),
                BaseCostsAnnual = 5 * 12,
                ConsumptionCostsPerKWh = 0.22M,
                NextPricingTier = null, // no next pricing tier
                KWhLimit = null, // no kwHLimit
            });

            dict.Add("pdt_1", new TariffEntity()
            {
                Id = Guid.NewGuid(),
                BaseCostsAnnual = 0,
                ConsumptionCostsPerKWh = 0.30M,
                NextPricingTier = null,
                KWhLimit = null,
            });

            dict.Add("pdt_0", new TariffEntity()
            {
                Id = Guid.NewGuid(),
                BaseCostsAnnual = 800M,
                ConsumptionCostsPerKWh = 0,
                NextPricingTier = dict["pdt_1"], // Reference next higher pricing tier
                KWhLimit = 4000M,
            });


            foreach (TariffEntity t in dict.Values)
            {
                context.Tariffs.Add(t);
            }
            context.SaveChanges();
        }

        private static void initProducts(TariffComparisonContext context, IDictionary<string, TariffEntity> tariffs)
        {
            ProductEntity[] productEntities = new ProductEntity[]
            {
                new ProductEntity() { 
                    Id = Guid.NewGuid(),
                    Name = "basic electricity tariff",
                    InitialTariff = tariffs["bet_0"],
                },
                new ProductEntity() {
                    Id = Guid.NewGuid(),
                    Name = "Packaged tariff",
                    InitialTariff = tariffs["pdt_0"],
                },
            };
            foreach(ProductEntity p in productEntities)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();
        }
        #endregion
    }
}

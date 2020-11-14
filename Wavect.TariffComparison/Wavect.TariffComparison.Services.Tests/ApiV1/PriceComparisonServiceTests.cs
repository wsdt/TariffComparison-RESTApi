using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wavect.TariffComparison.DTOs.ApiV1;
using Wavect.TariffComparison.DTOs.ApiVersionNeutral.Responses;
using Wavect.TariffComparison.Entities.Persisted;
using Wavect.TariffComparison.Persistence.Context;
using Wavect.TariffComparison.Services.ApiV1;

namespace Wavect.TariffComparison.Services.Tests.ApiV1
{
    public class PriceComparisonServiceTests
    {
        #region fields
        private DbContextOptions<TariffComparisonContext> _opts;
        private PriceComparisonService _priceComparisonService;
        private Guid _testProductId_OnDemand;
        private Guid _testProductId_Packaged;
        #endregion

        #region methods_lifecycle
        [SetUp]
        public void Setup()
        {
            _opts = new DbContextOptionsBuilder<TariffComparisonContext>()
                .UseInMemoryDatabase(databaseName: "TEST_PriceComparisonService")
                .Options;

            using (TariffComparisonContext context = new TariffComparisonContext(_opts))
            {
                // Insert test data
                initTariffs(context, out IDictionary<string, TariffEntity> tariffs);
                initProducts(context, tariffs);
            }

            // Init service for test (separate context)
            _priceComparisonService = new PriceComparisonService(new TariffComparisonContext(_opts));
        }

        [TearDown]
        public void TearDown()
        {
            using TariffComparisonContext context = new TariffComparisonContext(_opts);
            context.Database.EnsureDeleted();
        }
        #endregion

        #region tests
        [Test]
        public async Task GetCheapestFor_ProductTariffLst_SortedLstOfCheapestProductsFor3500KWh()
        {
            List<ProductTariffDTO> productTariffs = await _priceComparisonService.GetCheapestFor(3500);
            
            Assert.IsNotNull(productTariffs, "Got null, this should never happen as even an empty database should result an empty list.");
            Assert.AreEqual(2, productTariffs.Count);
            Assert.AreEqual(800, productTariffs[0].AnnualCosts, "800 € should be the cheapest tariff for this consumption!");
            Assert.AreEqual(_testProductId_Packaged, productTariffs[0].Id, "Calculation seems to be inverse somehow. Packaged tariff should be the cheapest.");
            Assert.AreEqual(830, productTariffs[1].AnnualCosts, "830 € should be the most expensive tariff for this consumption!");
            Assert.AreEqual(_testProductId_OnDemand, productTariffs[1].Id, "Calculation seems to be inverse somehow. OnDemand tariff should be the most expensive.");
        }

        [Test]
        public async Task GetCheapestFor_ProductTariffLst_SortedLstOfCheapestProductsFor4500KWh()
        {
            List<ProductTariffDTO> productTariffs = await _priceComparisonService.GetCheapestFor(4500);

            Assert.IsNotNull(productTariffs, "Got null, this should never happen as even an empty database should result an empty list.");
            Assert.AreEqual(2, productTariffs.Count);
            Assert.AreEqual(950, productTariffs[0].AnnualCosts, "950 € should be the cheapest tariff for this consumption!");
            Assert.AreEqual(_testProductId_Packaged, productTariffs[0].Id, "Calculation seems to be inverse somehow. Packaged tariff should be the cheapest.");
            Assert.AreEqual(1050, productTariffs[1].AnnualCosts, "1050 € should be the most expensive tariff for this consumption!");
            Assert.AreEqual(_testProductId_OnDemand, productTariffs[1].Id, "Calculation seems to be inverse somehow. OnDemand tariff should be the most expensive.");
        }

        [Test]
        public async Task GetCheapestFor_ProductTariffLst_SortedLstOfCheapestProductsFor6000KWh()
        {
            List<ProductTariffDTO> productTariffs = await _priceComparisonService.GetCheapestFor(6000);

            Assert.IsNotNull(productTariffs, "Got null, this should never happen as even an empty database should result an empty list.");
            Assert.AreEqual(2, productTariffs.Count);
            Assert.AreEqual(1380, productTariffs[0].AnnualCosts, "1380 € should be the cheapest tariff for this consumption!");
            Assert.AreEqual(_testProductId_OnDemand, productTariffs[0].Id, "Calculation seems to be inverse somehow. OnDemand tariff should be the cheapest.");
            Assert.AreEqual(1400, productTariffs[1].AnnualCosts, "1400 € should be the most expensive tariff for this consumption!");
            Assert.AreEqual(_testProductId_Packaged, productTariffs[1].Id, "Calculation seems to be inverse somehow. Packaged tariff should be the most expensive.");
        }
        #endregion

        #region methods_helpers
        private void initTariffs(TariffComparisonContext context, out IDictionary<string, TariffEntity> dict)
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

        private void initProducts(TariffComparisonContext context, IDictionary<string, TariffEntity> tariffs)
        {
            _testProductId_OnDemand = Guid.NewGuid();
            _testProductId_Packaged = Guid.NewGuid();

            ProductEntity[] productEntities = new ProductEntity[]
            {
                new ProductEntity() {
                    Id = _testProductId_OnDemand,
                    Name = "basic electricity tariff",
                    InitialTariff = tariffs["bet_0"],
                },
                new ProductEntity() {
                    Id = _testProductId_Packaged,
                    Name = "Packaged tariff",
                    InitialTariff = tariffs["pdt_0"],
                },
            };
            foreach (ProductEntity p in productEntities)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();
        }
        #endregion
    }
}
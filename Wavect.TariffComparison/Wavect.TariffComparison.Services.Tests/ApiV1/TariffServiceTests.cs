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
    public class TariffServiceTests
    {
        #region fields
        private DbContextOptions<TariffComparisonContext> _opts;
        private TariffService _tariffService;
        private Guid _testTariffId;
        private decimal _baseCostsAnnualTest = 800M;
        private decimal _consumptionCostsPerKwHNextTier = 0.30M;
        #endregion

        #region methods_lifecycle
        [SetUp]
        public void Setup()
        {
            _opts = new DbContextOptionsBuilder<TariffComparisonContext>()
                .UseInMemoryDatabase(databaseName: "TEST_TariffService")
                .Options;

            // Set id for class to access in tests (to have a specific reference)
            _testTariffId = Guid.NewGuid();

            using (TariffComparisonContext context = new TariffComparisonContext(_opts))
            {
                // Insert test data
                // Using dict here and single assignment to have it easier to assign corresponding relationships
                Dictionary<string, TariffEntity> dict = new Dictionary<string, TariffEntity>();

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
                    ConsumptionCostsPerKWh = _consumptionCostsPerKwHNextTier,
                    NextPricingTier = null,
                    KWhLimit = null,
                });

                dict.Add("pdt_0", new TariffEntity()
                {
                    Id = _testTariffId,
                    BaseCostsAnnual = _baseCostsAnnualTest,
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

            // Init service for test (separate context)
            _tariffService = new TariffService(new TariffComparisonContext(_opts));
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
        public async Task Get_Tariff_CorrectData()
        {
            Assert.IsNotNull(_testTariffId, "The testProduct id has to be defined");
            TariffDTO tariffDTO = await _tariffService.Get(_testTariffId);
            
            Assert.IsNotNull(tariffDTO, "Test tariff NOT found!");
            Assert.AreEqual(_baseCostsAnnualTest, tariffDTO.BaseCostsAnnual, "Something is wrong with your search. Annual costs do not equal.");
            Assert.NotNull(tariffDTO.NextPricingTier, "Next pricing tier should be defined, got null.");
            Assert.AreEqual(_consumptionCostsPerKwHNextTier, tariffDTO.NextPricingTier.ConsumptionCostsPerKWh, "The next pricing tier seems to wrong. Consumption per kWh does not match.");
        }

        [Test]
        public async Task Get_AllTariffs_CorrectData()
        {
            List<TariffDTO> tariffDTOs = await _tariffService.GetAll();

            Assert.IsNotNull(tariffDTOs, "This method should at least return an empty list, but got null.");
            Assert.AreEqual(3, tariffDTOs.Count, $"Got an unexpected amount of tariffs. Got: {tariffDTOs.Count}"); // exactly products
            Assert.AreEqual(_baseCostsAnnualTest, tariffDTOs[2].BaseCostsAnnual, "Base costs do not match.");
            Assert.NotNull(tariffDTOs[2].NextPricingTier, "Next pricing tier should be defined, got null.");
            Assert.AreEqual(_consumptionCostsPerKwHNextTier, tariffDTOs[2].NextPricingTier.ConsumptionCostsPerKWh, "The next pricing tier seems to wrong.Consumption per kWh does not match.");
        }
        #endregion
    }
}
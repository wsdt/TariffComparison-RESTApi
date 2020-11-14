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
    public class ProductSerivceTests
    {
        #region fields
        private DbContextOptions<TariffComparisonContext> _opts;
        private ProductService _productService;
        private Guid _testProductId;
        private decimal _testBaseCostsAnnual = 800M;
        private const string _testProductName = "Packaged tariff"; // needed as dtos usually do not contain Id
        #endregion

        #region methods_lifecycle
        [SetUp]
        public void Setup()
        {
             _opts = new DbContextOptionsBuilder<TariffComparisonContext>()
                .UseInMemoryDatabase(databaseName: "TEST_ProductService")
                .Options;

            // Set id for class to access in tests (to have a specific reference)
            _testProductId = Guid.NewGuid();

            using (TariffComparisonContext context = new TariffComparisonContext(_opts))
            {
                // Insert test data
                context.Products.AddRange(
                    new ProductEntity()
                    {
                        Id = Guid.NewGuid(),
                        Name = "basic electricity tariff",
                        InitialTariff = null,
                    },
                    new ProductEntity()
                    {
                        Id = _testProductId,
                        Name = "Packaged tariff",
                        InitialTariff = new TariffEntity()
                        {
                            Id = Guid.NewGuid(),
                            BaseCostsAnnual = 800M,
                            ConsumptionCostsPerKWh = 0,
                            NextPricingTier = null, // Reference next higher pricing tier
                            KWhLimit = 4000M,
                        },
                    }
                );
                context.SaveChanges();
            }

            // Init service for test (separate context)
            _productService = new ProductService(new TariffComparisonContext(_opts));
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
        public async Task Get_Product_CorrectData()
        {
            Assert.IsNotNull(_testProductId, "The testProduct id has to be defined");
            ProductDTO productDTO = await _productService.Get(_testProductId);
            
            Assert.IsNotNull(productDTO, "Test product NOT found!");
            Assert.AreEqual(_testProductName, productDTO.Name, "Something is wrong with your ID search.");
            Assert.IsNotNull(productDTO.InitialTariff, "Initial tariff should be defined, but is null.");
            Assert.AreEqual(_testBaseCostsAnnual, productDTO.InitialTariff.BaseCostsAnnual, "Annual base costs do not equal.");
        }

        [Test]
        public async Task Get_AllProducts_CorrectData()
        {
            List<ProductDTO> productDTOs = await _productService.GetAll();

            Assert.IsNotNull(productDTOs, "This method should at least return an empty list, but got null.");
            Assert.AreEqual(2, productDTOs.Count, $"Got an unexpected amount of products. Got: {productDTOs.Count}"); // exactly products
            Assert.AreNotEqual(productDTOs[0], productDTOs[1], "The same product has been saved twice."); // no duplicate saved
            Assert.AreEqual(_testProductName, productDTOs[1].Name, "The default order somehow got screwed up, or faulty value has been saved.");
        }
        #endregion
    }
}
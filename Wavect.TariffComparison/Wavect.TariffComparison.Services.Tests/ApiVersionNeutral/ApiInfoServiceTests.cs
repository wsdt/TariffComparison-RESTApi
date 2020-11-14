using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Wavect.TariffComparison.DTOs.ApiVersionNeutral.Responses;
using Wavect.TariffComparison.Persistence.Context;
using Wavect.TariffComparison.Services.ApiVersionNeutral;

namespace Wavect.TariffComparison.Services.Tests.ApiVersionNeutral
{
    public class ApiInfoServiceTests
    {
        #region fields
        private ApiInfoService _apiInfoService;
        private DbContextOptions<TariffComparisonContext> _opts;
        #endregion

        #region methods_lifecycle
        [SetUp]
        public void Setup()
        {
            _opts = new DbContextOptionsBuilder<TariffComparisonContext>()
                .UseInMemoryDatabase(databaseName: "TEST_TariffService")
                .Options;

            _apiInfoService = new ApiInfoService(new TariffComparisonContext(_opts));
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
        public async Task Get_ApiInfo_Healthy()
        {
            ApiInfoDTO dto = await _apiInfoService.Get();
            Assert.IsNotNull(dto);
            Assert.IsTrue(dto.IsApiHealthy);
            Assert.IsNotEmpty(dto.LatestApiVersion);
        }
        #endregion
    }
}
using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wavect.TariffComparison.Services.ApiV1;
using Wavect.TariffComparison.Services.ApiV1.Interfaces;
using Wavect.TariffComparison.Services.ApiVersionNeutral;
using Wavect.TariffComparison.Services.ApiVersionNeutral.Interfaces;

namespace Wavect.TariffComparison.WebAPI
{
    /// <summary>
    /// Injects services and other types for general usage.
    /// </summary>
    public class DependencyInjectionComposition : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            #region non-scoped injectables
            serviceRegistry.Register<IApiInfoService, ApiInfoService>();
            serviceRegistry.Register<IProductService, ProductService>();
            serviceRegistry.Register<ITariffService, TariffService>();
            serviceRegistry.Register<IPriceComparisonService, PriceComparisonService>();
            #endregion
        }
    }
}

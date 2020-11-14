using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wavect.TariffComparison.DTOs.ApiV1;

namespace Wavect.TariffComparison.Services.ApiV1.Interfaces
{
    public interface IProductService : IRestfulEntityService<ProductDTO>
    {
    }
}

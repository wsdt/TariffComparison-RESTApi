using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wavect.TariffComparison.Interfaces.DTOs;

namespace Wavect.TariffComparison.Services.ApiV1.Interfaces
{
    /// <summary>
    /// Base interface for other restful api controllers that directly relate to an entity, to reduce code duplicates.
    /// </summary>
    public interface IRestfulEntityService<T>
        where T : IBaseDTO
    {
        #region GET
        public Task<T> Get(Guid id);
        public Task<List<T>> GetAll();
        #endregion
    }
}
